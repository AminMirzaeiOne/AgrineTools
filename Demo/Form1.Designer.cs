namespace Demo
{
    partial class Form1
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
            this.agComboBox1 = new AgrineUI.Controls.Foundation.AGComboBox();
            this.SuspendLayout();
            // 
            // agComboBox1
            // 
            this.agComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.agComboBox1.BorderRadius = 8;
            this.agComboBox1.BorderSize = 2;
            this.agComboBox1.DarkMode = true;
            this.agComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.agComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.agComboBox1.ForeColor = System.Drawing.Color.White;
            this.agComboBox1.FormattingEnabled = true;
            this.agComboBox1.Items.AddRange(new object[] {
            "Google",
            "Microsoft",
            "FaceBook",
            "Apple"});
            this.agComboBox1.Location = new System.Drawing.Point(172, 169);
            this.agComboBox1.Name = "agComboBox1";
            this.agComboBox1.Palette = System.Drawing.Color.SkyBlue;
            this.agComboBox1.Size = new System.Drawing.Size(336, 35);
            this.agComboBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(835, 667);
            this.Controls.Add(this.agComboBox1);
            this.DarkMode = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Palette = System.Drawing.Color.SkyBlue;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AgrineUI.Controls.Foundation.AGComboBox agComboBox1;
    }
}

