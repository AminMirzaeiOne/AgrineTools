namespace Demo
{
    partial class StartForm
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
            this.agFormControl1 = new AgrineUI.Controls.Partial.AGFormControl();
            this.SuspendLayout();
            // 
            // agFormControl1
            // 
            this.agFormControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agFormControl1.BorderRadius = ((byte)(20));
            this.agFormControl1.BorderSize = ((byte)(0));
            this.agFormControl1.DarkMode = true;
            this.agFormControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.agFormControl1.ForeColor = System.Drawing.Color.White;
            this.agFormControl1.Location = new System.Drawing.Point(5, 5);
            this.agFormControl1.Name = "agFormControl1";
            this.agFormControl1.Palette = System.Drawing.Color.Tomato;
            this.agFormControl1.Size = new System.Drawing.Size(923, 48);
            this.agFormControl1.TabIndex = 0;
            // 
            // StartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(933, 516);
            this.Controls.Add(this.agFormControl1);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private AgrineUI.Controls.Partial.AGFormControl agFormControl1;
    }
}