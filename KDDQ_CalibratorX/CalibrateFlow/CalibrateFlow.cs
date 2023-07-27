using KDDQ_CalibratorX.MetersStandards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalibrateFlow
{
    public class CalibrateFlow
    {
        public class CalibrateFlowNode
        {
            public string OperationType { get; set; }
            public string ClassName { get; set; }
            public string PortName { get; set; }
            public string Keyword { get; set; }
            public string LinkTarget { get; set; }
            public int DelayInterval { get; set; }
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
                    DelayInterval = int.Parse(gv.Rows[5].Cells[i].Value.ToString())
                };

                nodes.Add(node);
            }

            return nodes;
        }


        public async Task CalibrateFlowOpeartion(Krypton.Toolkit.KryptonDataGridView gv1, Krypton.Toolkit.KryptonDataGridView gv2)
        {
            var calibrateFlowNodeList  = ReadCalibrateFlowNodes(gv1);

            // 创建设备工厂
            DeviceFactory deviceFactory = new DeviceFactory();

            // 遍历校准数据表格的行
            for (int i = 0; i < gv2.Rows.Count; i++)
            {
                // 获取当前行的数据
                DataGridViewRow dataRow = gv2.Rows[i];

                // 遍历校准数据表格的列
                for (int j = 0; j < gv2.Columns.Count; j++)
                {

                    CalibrateFlowNode config = calibrateFlowNodeList[j];
                    // 创建设备对象
                    IDevice device = deviceFactory.CreateDevice(config.ClassName, config.PortName);

                    // 执行读或写操作
                    if (config.OperationType == "Read")
                    {

                        // 发送读取指令
                        string readCommand = device.ParseReadCommand(step.Keyword);
                        await device.WriteAsync(readCommand);

                        // 读取数据
                        string data = await device.ReadAsync();
                        string result = device.ParseResponse(data);

                        // 将结果写入当前单元格
                        dataRow.Cells[j].Value = result;
                        dataRow.Cells[j].Style.BackColor = Color.Green;
                    }
                    else if (config.OperationType == "Write")
                    {
                        // 获取当前单元格的数据
                        string value = dataRow.Cells[j].Value.ToString();

                        // 格式化写入指令
                        string writeCommand = device.ParseWriteCommand(step.Keyword, value);
                        await device.WriteAsync(writeCommand);

                        // 将结果写入当前单元格
                        dataRow.Cells[j].Value = "写入成功";
                        dataRow.Cells[j].Style.BackColor = Color.Green;
                    }
                    else if (config.OperationType == "Cal")
                    {

                    }
                }
            }
        }

    }
}
