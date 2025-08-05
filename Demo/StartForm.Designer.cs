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
            this.agCircleButton1 = new AgrineUI.Controls.Advanced.AGCircleButton();
            this.agCircleButton2 = new AgrineUI.Controls.Advanced.AGCircleButton();
            this.agButton1 = new AgrineUI.Controls.Foundation.AGButton();
            this.SuspendLayout();
            // 
            // agCircleButton1
            // 
            this.agCircleButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agCircleButton1.BorderSize = ((byte)(2));
            this.agCircleButton1.DarkMode = true;
            this.agCircleButton1.DefaultButton = false;
            this.agCircleButton1.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agCircleButton1.FlatAppearance.BorderSize = 0;
            this.agCircleButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agCircleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agCircleButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agCircleButton1.ForeColor = System.Drawing.Color.White;
            this.agCircleButton1.Location = new System.Drawing.Point(328, 198);
            this.agCircleButton1.Name = "agCircleButton1";
            this.agCircleButton1.Palette = System.Drawing.Color.Tomato;
            this.agCircleButton1.Size = new System.Drawing.Size(179, 179);
            this.agCircleButton1.TabIndex = 0;
            this.agCircleButton1.Text = "agCircleButton1";
            this.agCircleButton1.UseVisualStyleBackColor = false;
            // 
            // agCircleButton2
            // 
            this.agCircleButton2.BackColor = System.Drawing.Color.Tomato;
            this.agCircleButton2.BorderSize = ((byte)(2));
            this.agCircleButton2.DarkMode = true;
            this.agCircleButton2.DefaultButton = true;
            this.agCircleButton2.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agCircleButton2.FlatAppearance.BorderSize = 0;
            this.agCircleButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(99)))), ((int)(((byte)(71)))));
            this.agCircleButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agCircleButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agCircleButton2.ForeColor = System.Drawing.Color.Black;
            this.agCircleButton2.Location = new System.Drawing.Point(770, 290);
            this.agCircleButton2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.agCircleButton2.Name = "agCircleButton2";
            this.agCircleButton2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.agCircleButton2.Palette = System.Drawing.Color.Tomato;
            this.agCircleButton2.Size = new System.Drawing.Size(48, 48);
            this.agCircleButton2.TabIndex = 1;
            this.agCircleButton2.Text = "x";
            this.agCircleButton2.UseVisualStyleBackColor = false;
            // 
            // agButton1
            // 
            this.agButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton1.BorderRadius = ((byte)(20));
            this.agButton1.BorderSize = ((byte)(2));
            this.agButton1.DarkMode = true;
            this.agButton1.DefaultButton = false;
            this.agButton1.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton1.FlatAppearance.BorderSize = 0;
            this.agButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton1.ForeColor = System.Drawing.Color.White;
            this.agButton1.Location = new System.Drawing.Point(511, 56);
            this.agButton1.Name = "agButton1";
            this.agButton1.Palette = System.Drawing.Color.Tomato;
            this.agButton1.Size = new System.Drawing.Size(195, 94);
            this.agButton1.TabIndex = 2;
            this.agButton1.Text = "agButton1";
            this.agButton1.UseVisualStyleBackColor = false;
            // 
            // StartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(933, 516);
            this.Controls.Add(this.agButton1);
            this.Controls.Add(this.agCircleButton2);
            this.Controls.Add(this.agCircleButton1);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private AgrineUI.Controls.Advanced.AGCircleButton agCircleButton1;
        private AgrineUI.Controls.Advanced.AGCircleButton agCircleButton2;
        private AgrineUI.Controls.Foundation.AGButton agButton1;
    }
}