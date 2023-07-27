namespace KDDQ_CalibratorX
{
    partial class FormLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonListView1 = new Krypton.Toolkit.KryptonListView();
            this.SuspendLayout();
            // 
            // kryptonListView1
            // 
            this.kryptonListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonListView1.HideSelection = false;
            this.kryptonListView1.ItemStyle = Krypton.Toolkit.ButtonStyle.ListItem;
            this.kryptonListView1.Location = new System.Drawing.Point(0, 0);
            this.kryptonListView1.Name = "kryptonListView1";
            this.kryptonListView1.OwnerDraw = true;
            this.kryptonListView1.Size = new System.Drawing.Size(800, 450);
            this.kryptonListView1.StateCommon.Item.Content.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.kryptonListView1.StateCommon.Item.Content.ShortText.MultiLineH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonListView1.StateCommon.Item.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonListView1.TabIndex = 0;
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonListView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLog";
            this.Text = "FormLog";
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonListView kryptonListView1;
    }
}