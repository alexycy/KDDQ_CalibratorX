using Krypton.Docking;
using Krypton.Toolkit;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDDQ_CalibratorX
{
    public partial class FormMain : Krypton.Toolkit.KryptonForm
    {

        FormGrid formGrid;
        FormConfig formConfig;
        FormLog formLog;
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

        }

        private void 打开数据文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               GridViewOperation.LoadExcelFile(openFileDialog.FileName, formGrid.DataGridView1,formConfig.DataGridView1);
               //GridViewOperation.LoadExcelFile(openFileDialog.FileName,kryptonPropertyGrid1);
            }
        }


    }
}
