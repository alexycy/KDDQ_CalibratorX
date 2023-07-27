using AuxiliaryMeans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDDQ_CalibratorX
{
    public partial class FormGrid : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FormGrid()
        {
            InitializeComponent();
        }

        private void FormGrid_Load(object sender, EventArgs e)
        {
            this.Text = "校准数据";
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("删除单元格", null, (s, h) => GridViewOperation.DeleteCell(sender, e, kryptonDataGridView1));
            contextMenuStrip.Items.Add("删除行", null, (s, h) => GridViewOperation.DeleteRow(sender, e, kryptonDataGridView1));
            contextMenuStrip.Items.Add("添加行", null, (s, h) => GridViewOperation.AddRow(sender, e, kryptonDataGridView1));
            contextMenuStrip.Items.Add("复制单元格", null, (s, h) => GridViewOperation.CopyCell(sender, e, kryptonDataGridView1));


            kryptonDataGridView1.ContextMenuStrip = contextMenuStrip;
        }

        public Krypton.Toolkit.KryptonDataGridView DataGridView1
        {
            get { return kryptonDataGridView1; }
        }
    }
}
