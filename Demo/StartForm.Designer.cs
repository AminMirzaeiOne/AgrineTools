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
            this.agButton1 = new AgrineUI.Controls.Foundation.AGButton();
            this.agCircleButton1 = new AgrineUI.Controls.Advanced.AGCircleButton();
            this.agButton2 = new AgrineUI.Controls.Foundation.AGButton();
            this.SuspendLayout();
            // 
            // agButton1
            // 
            this.agButton1.Animation = true;
            this.agButton1.AnimationSpeed = AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions.VerySlow;
            this.agButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agButton1.BorderRadius = ((byte)(30));
            this.agButton1.BorderSize = ((byte)(4));
            this.agButton1.DarkMode = true;
            this.agButton1.DefaultButton = false;
            this.agButton1.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton1.FlatAppearance.BorderSize = 0;
            this.agButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton1.ForeColor = System.Drawing.Color.White;
            this.agButton1.Location = new System.Drawing.Point(316, 167);
            this.agButton1.Name = "agButton1";
            this.agButton1.Palette = System.Drawing.Color.Tomato;
            this.agButton1.Size = new System.Drawing.Size(289, 129);
            this.agButton1.Sound = false;
            this.agButton1.TabIndex = 0;
            this.agButton1.Text = "agButton1";
            this.agButton1.UseVisualStyleBackColor = false;
            // 
            // agCircleButton1
            // 
            this.agCircleButton1.Animation = true;
            this.agCircleButton1.AnimationSpeed = AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions.VeryQuick;
            this.agCircleButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agCircleButton1.BorderSize = ((byte)(5));
            this.agCircleButton1.DarkMode = true;
            this.agCircleButton1.DefaultButton = false;
            this.agCircleButton1.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agCircleButton1.FlatAppearance.BorderSize = 0;
            this.agCircleButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agCircleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agCircleButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agCircleButton1.ForeColor = System.Drawing.Color.White;
            this.agCircleButton1.Location = new System.Drawing.Point(736, 275);
            this.agCircleButton1.Name = "agCircleButton1";
            this.agCircleButton1.Palette = System.Drawing.Color.CornflowerBlue;
            this.agCircleButton1.Size = new System.Drawing.Size(127, 127);
            this.agCircleButton1.Sound = true;
            this.agCircleButton1.TabIndex = 1;
            this.agCircleButton1.Text = "sd";
            this.agCircleButton1.UseVisualStyleBackColor = false;
            // 
            // agButton2
            // 
            this.agButton2.Animation = true;
            this.agButton2.AnimationSpeed = AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions.VeryQuick;
            this.agButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agButton2.BorderRadius = ((byte)(30));
            this.agButton2.BorderSize = ((byte)(4));
            this.agButton2.DarkMode = true;
            this.agButton2.DefaultButton = false;
            this.agButton2.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton2.FlatAppearance.BorderSize = 0;
            this.agButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.agButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton2.ForeColor = System.Drawing.Color.White;
            this.agButton2.Location = new System.Drawing.Point(316, 318);
            this.agButton2.Name = "agButton2";
            this.agButton2.Palette = System.Drawing.Color.Tomato;
            this.agButton2.Size = new System.Drawing.Size(289, 129);
            this.agButton2.Sound = false;
            this.agButton2.TabIndex = 0;
            this.agButton2.Text = "agButton1";
            this.agButton2.UseVisualStyleBackColor = false;
            // 
            // StartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(933, 516);
            this.Controls.Add(this.agCircleButton1);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton1);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private AgrineUI.Controls.Foundation.AGButton agButton1;
        private AgrineUI.Controls.Advanced.AGCircleButton agCircleButton1;
        private AgrineUI.Controls.Foundation.AGButton agButton2;
    }
}