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
    public partial class FormLog : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FormLog()
        {
            InitializeComponent();
            this.Text = "校准日志";
        }
        public Krypton.Toolkit.KryptonListView ListView1
        {
            get { return kryptonListView1; }
        }
    }
}
