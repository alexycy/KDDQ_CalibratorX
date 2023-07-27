using Krypton.Toolkit;
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
    public partial class SelectionForm : Krypton.Toolkit.KryptonForm
    {
        public string _selectedOption;
        public SelectionForm()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            _selectedOption = "选择";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string SelectedOption
        {
            get { return _selectedOption; }
        }
    }
}
