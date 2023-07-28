using KDDQ_CalibratorX.AuxiliaryMeans;
using KDDQ_CalibratorX.MetersStandards;
using KDDQ_CalibratorX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace CalibrateFlow
{
    public class CalibrateFlowOperation
    {
        

        public class CalibrateFlowNode
        {
            public string OperationType { get; set; }
            public string ClassName { get; set; }
            public string PortName { get; set; }
            public string Keyword { get; set; }
            public string LinkTarget { get; set; }
            public string DelayInterval { get; set; }
            public string formula { get; set; }
        }

        public List<CalibrateFlowNode> ReadCalibrateFlowNodes(Krypton.Toolkit.KryptonDataGridView gv)
        {
            List<CalibrateFlowNode> nodes = new List<CalibrateFlowNode>();

            for (int i = 0; i < gv.ColumnCount; i++)
            {
                CalibrateFlowNode node = new CalibrateFlowNode
                {
                    OperationType = gv.Rows[0].Cells[i].Value.ToString(),
                    ClassName = gv.Rows[1].Cells[i].Value.ToString(),
                    PortName = gv.Rows[2].Cells[i].Value.ToString(),
                    Keyword = gv.Rows[3].Cells[i].Value.ToString(),
                    LinkTarget = gv.Rows[4].Cells[i].Value.ToString(),
                    DelayInterval = gv.Rows[5].Cells[i].Value.ToString(),
                    formula = gv.Rows[6].Cells[i].Value.ToString()
                };

                nodes.Add(node);
            }

            return nodes;
        }


        public async Task CalibrateFlowOpeartion(Krypton.Toolkit.KryptonDataGridView gv1, Krypton.Toolkit.KryptonDataGridView gv2, CancellationTokenSource _cts, SemaphoreSlim _pauseSemaphore)
        {
            var calibrateFlowNodeList  = ReadCalibrateFlowNodes(gv1);

            // 获取Unity容器
            var container = UnityContainerManager.GetContainer();

            // 遍历校准数据表格的行
            for (int i = 0; i < gv2.Rows.Count; i++)
            {
                // 在循环的开始处检查是否应该取消操作
                if (_cts.Token.IsCancellationRequested)
                {
                    // 如果应该取消操作，那么抛出一个OperationCanceledException
                    return;
                }
                // 在这里暂停任务
                await _pauseSemaphore.WaitAsync();
                _pauseSemaphore.Release();
                // 获取当前行的数据
                DataGridViewRow dataRow = gv2.Rows[i];

                // 遍历校准数据表格的列
                for (int j = 0; j < gv2.Columns.Count; j++)
                {

                    CalibrateFlowNode config = calibrateFlowNodeList[j];
                    // 创建设备对象
                    //Type deviceType = Type.GetType("KDDQ_CalibratorX.MetersStandards." + config.ClassName);
                    //IDevice device = (IDevice)container.Resolve(deviceType);
                    IDevice device = (IDevice)container.Resolve<IDevice>(config.ClassName, new ParameterOverride("portName", config.PortName));

                    // 执行读或写操作
                    if (config.OperationType == "READ")
                    {
                        device.ConnectAsync(config.PortName);

                        // 发送读取指令
                        var readCommand = device.SplitAndPackageCommand("READ", "NAN");
                        for (int k = 0; k < readCommand.Count; k++)
                        {
                            await device.WriteAsync(readCommand[k]);
                        }
                        

                        // 读取数据
                        var result = await device.ReadAsync(TimeSpan.FromMilliseconds(500));


                        // 将结果写入当前单元格
                        dataRow.Cells[j].Value = result;
                        dataRow.Cells[j].Style.BackColor = Color.Green;
                        device.DisconnectAsync();
                    }
                    else if (config.OperationType == "WRITE")
                    {
                        device.ConnectAsync(config.PortName);

                        // 获取当前单元格的数据
                        var value = dataRow.Cells[j].Value.ToString();

                        // 格式化写入指令
                        var readCommand = device.SplitAndPackageCommand(config.Keyword, value);
                        for (int k = 0; k < readCommand.Count; k++)
                        {
                            await device.WriteAsync(readCommand[k]);
                        }
                        // 将结果写入当前单元格
                        dataRow.Cells[j].Style.BackColor = Color.Green;
                        device.DisconnectAsync();
                    }
                    else if (config.OperationType == "CAL")
                    {
                        // 获取计算公式
                        string formula = config.Keyword;

                        // 创建一个新的DataTable
                        var table = new System.Data.DataTable();

                        // 使用正则表达式匹配公式中的变量名
                        var matches = Regex.Matches(formula, @"[a-zA-Z_][a-zA-Z0-9_]*");

                        foreach (Match match in matches)
                        {
                            // 获取变量名
                            string variableName = match.Value;

                            // 从当前行获取变量的值
                            double variableValue = Convert.ToDouble(dataRow.Cells[variableName].Value);

                            // 将变量的值添加到DataTable中
                            table.Columns.Add(variableName, typeof(double), variableValue.ToString());
                        }

                        // 计算公式
                        double result = Convert.ToDouble(table.Compute(formula, null));

                        // 将结果写入当前单元格
                        dataRow.Cells[j].Value = result;
                        dataRow.Cells[j].Style.BackColor = Color.Green;
                    }


                    //根据当前CalibrateFlowNode的延时时间设置延时
                    Thread.Sleep(Convert.ToInt16( config.DelayInterval.Trim()));
                }
            }
        }

    }
}
