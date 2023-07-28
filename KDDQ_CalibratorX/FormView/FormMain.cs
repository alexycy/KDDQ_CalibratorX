using AuxiliaryMeans;
using CalibrateFlow;
using KDDQ_CalibratorX.AuxiliaryMeans;
using Krypton.Docking;
using Krypton.Toolkit;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Timer = System.Windows.Forms.Timer;

namespace KDDQ_CalibratorX
{
    public partial class FormMain : Krypton.Toolkit.KryptonForm
    {

        FormGrid formGrid;
        FormConfig formConfig;
        FormLog formLog;
        private bool _isRunning;
        CancellationTokenSource cts;
        SemaphoreSlim pauseSemaphore;
        private bool _isPause;
        Timer timer1;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dockPanel1.Theme = new WeifenLuo.WinFormsUI.Docking.VS2003Theme();
            formGrid = new FormGrid();
            formGrid.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            formConfig = new FormConfig();
            formConfig.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            formLog = new FormLog();
            formLog.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);

            formGrid.Activate();

            // 将ToolStripProgressBar的宽度设置为StatusStrip的宽度减去其他控件的宽度
            toolStripProgressBar1.Width = kryptonStatusStrip1.Width - toolStripStatusLabel1.Width - 20; // 20是预留的间隙

            _isPause = false;   
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if(toolStripProgressBar1.Value<100)
                toolStripProgressBar1.Value += 20;
            else
                toolStripProgressBar1.Value =0;
        }

        private async void 打开数据文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                timer1 = new Timer();
                timer1.Interval = 200;
                timer1.Tick += Timer1_Tick;
                timer1.Start();
                // 在后台线程中加载Excel文件
                await Task.Run(() =>
                {
                    this.Invoke((Action)(() => GridViewOperation.LoadExcelFile(openFileDialog.FileName, formGrid.DataGridView1, formConfig.DataGridView1)));
                });
                timer1.Stop();
                toolStripProgressBar1.Value = 100;
            }
        }


        public async Task CalibrateStart()
        {
            // 获取Unity容器
            var container = UnityContainerManager.GetContainer();

            // 解析CalibrateFlowOperation的单例
            var calibrateFlowOperation = container.Resolve<CalibrateFlowOperation>();
            pauseSemaphore = new SemaphoreSlim(1, 1);

            // 使用calibrateFlowOperation来调用它的方法
            cts = new CancellationTokenSource();


            // 在后台线程中启动校准任务
            await Task.Run(() =>
            {
                this.Invoke((Action)(() => calibrateFlowOperation.CalibrateFlowOpeartion(formConfig.DataGridView1, formGrid.DataGridView1, cts, pauseSemaphore)));
            });
        }

        private async void toolStripButton3_Click(object sender, EventArgs e)
        {
            // 获取触发事件的按钮
            var button = (ToolStripButton)sender;

            if (!_isRunning)
            {


                // 启动任务
                await CalibrateStart();

                // 改变按钮的文本
                button.Text = "启动";

                // 更新任务的状态
                _isRunning = true;
            }
            else
            {
                // 停止任务
                 CalibrateStop();

                // 改变按钮的文本
                button.Text = "停止";

                // 更新任务的状态
                _isRunning = false;
            }
        }

        private void CalibrateStop()
        {
            cts.Cancel();
        }

        private void CalibrateRun()
        {
            throw new NotImplementedException();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // 获取触发事件的按钮
            var button = (ToolStripButton)sender;
            if (!_isPause)
            {
                pauseSemaphore.Wait();
            }
            else
            {
                pauseSemaphore.Release();
            }
        }

        private void kryptonStatusStrip1_Layout(object sender, LayoutEventArgs e)
        {
          
        }

        private void kryptonStatusStrip1_Resize(object sender, EventArgs e)
        {
         
        }
    }
}
