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
    public partial class FormConfig : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FormConfig()
        {
            InitializeComponent();
            this.Text = "校准配置";
        }
        public Krypton.Toolkit.KryptonDataGridView DataGridView1
        {
            get { return kryptonDataGridView1; }
        }

        private void kryptonDataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {


            
        }

        private void kryptonDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            SelectionForm form = new SelectionForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                kryptonDataGridView1.CurrentCell.Value = form.SelectedOption;
            }
        }
    }
}
