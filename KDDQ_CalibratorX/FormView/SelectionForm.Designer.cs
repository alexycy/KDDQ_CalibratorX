namespace KDDQ_CalibratorX
{
    partial class SelectionForm
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
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonListBox2 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Location = new System.Drawing.Point(38, 52);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(248, 330);
            this.kryptonListBox1.TabIndex = 0;
            // 
            // kryptonListBox2
            // 
            this.kryptonListBox2.Location = new System.Drawing.Point(316, 52);
            this.kryptonListBox2.Name = "kryptonListBox2";
            this.kryptonListBox2.Size = new System.Drawing.Size(236, 319);
            this.kryptonListBox2.TabIndex = 1;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(606, 302);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(168, 69);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Values.Text = "确认";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 403);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.kryptonListBox2);
            this.Controls.Add(this.kryptonListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectionForm";
            this.Text = "SelectionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private Krypton.Toolkit.KryptonListBox kryptonListBox2;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}