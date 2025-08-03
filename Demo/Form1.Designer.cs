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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.agButton1 = new AgrineUI.Controls.AGButton();
            this.agButton2 = new AgrineUI.Controls.AGButton();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.switchButton1 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.agCircle1 = new AgrineUI.Shapes.AGCircle();
            this.agRadioButton1 = new AgrineUI.Controls.AGRadioButton();
            this.agRadioButton2 = new AgrineUI.Controls.AGRadioButton();
            this.agRadioButton3 = new AgrineUI.Controls.AGRadioButton();
            this.agCheckBox1 = new AgrineUI.Controls.AGCheckBox();
            this.agCheckBox2 = new AgrineUI.Controls.AGCheckBox();
            this.agSwitchButton1 = new AgrineUI.Controls.AGSwitchButton();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(12, 23);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(184, 85);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.Symbol = "";
            this.buttonX1.SymbolSize = 15F;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // agButton1
            // 
            this.agButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton1.BorderColor = System.Drawing.Color.MediumVioletRed;
            this.agButton1.BorderRadius = ((byte)(40));
            this.agButton1.BorderSize = ((byte)(3));
            this.agButton1.DefaultButton = true;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton1.Location = new System.Drawing.Point(253, 17);
            this.agButton1.Name = "agButton1";
            this.agButton1.Size = new System.Drawing.Size(246, 141);
            this.agButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.agButton1.Symbol = "57386";
            this.agButton1.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.agButton1.TabIndex = 1;
            this.agButton1.Text = "Unmiute";
            this.agButton1.Click += new System.EventHandler(this.agButton1_Click);
            // 
            // agButton2
            // 
            this.agButton2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton2.BackColor = System.Drawing.Color.Firebrick;
            this.agButton2.BorderColor = System.Drawing.Color.MediumVioletRed;
            this.agButton2.BorderRadius = ((byte)(40));
            this.agButton2.BorderSize = ((byte)(3));
            this.agButton2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.agButton2.DefaultButton = false;
            this.agButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton2.Location = new System.Drawing.Point(253, 196);
            this.agButton2.Name = "agButton2";
            this.agButton2.Size = new System.Drawing.Size(246, 141);
            this.agButton2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.agButton2.Symbol = "57387";
            this.agButton2.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.agButton2.TabIndex = 1;
            this.agButton2.Text = "Miute";
            // 
            // textBoxX1
            // 
            this.textBoxX1.BackColor = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.DisabledBackColor = System.Drawing.Color.Black;
            this.textBoxX1.ForeColor = System.Drawing.Color.White;
            this.textBoxX1.Location = new System.Drawing.Point(180, 504);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(233, 34);
            this.textBoxX1.TabIndex = 2;
            this.textBoxX1.WatermarkText = "Enter Text";
            // 
            // textBoxX2
            // 
            this.textBoxX2.BackColor = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.DisabledBackColor = System.Drawing.Color.Black;
            this.textBoxX2.ForeColor = System.Drawing.Color.White;
            this.textBoxX2.Location = new System.Drawing.Point(180, 559);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.PreventEnterBeep = true;
            this.textBoxX2.Size = new System.Drawing.Size(233, 34);
            this.textBoxX2.TabIndex = 2;
            this.textBoxX2.WatermarkText = "Enter Text";
            // 
            // switchButton1
            // 
            // 
            // 
            // 
            this.switchButton1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton1.Location = new System.Drawing.Point(23, 304);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.OffText = "خاموش";
            this.switchButton1.OnText = "روشن";
            this.switchButton1.Size = new System.Drawing.Size(102, 33);
            this.switchButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton1.TabIndex = 4;
            // 
            // agCircle1
            // 
            this.agCircle1.AgreeTheme = false;
            this.agCircle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.agCircle1.BorderColor = System.Drawing.Color.MediumVioletRed;
            this.agCircle1.BorderSize = ((byte)(2));
            this.agCircle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agCircle1.Location = new System.Drawing.Point(23, 396);
            this.agCircle1.Name = "agCircle1";
            this.agCircle1.Size = new System.Drawing.Size(124, 124);
            this.agCircle1.TabIndex = 5;
            // 
            // agRadioButton1
            // 
            this.agRadioButton1.AutoSize = true;
            this.agRadioButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.agRadioButton1.BorderSize = 3F;
            this.agRadioButton1.CircleSizeMode = AgrineUI.Controls.BoxSizeMode.VeryLarge;
            this.agRadioButton1.ForeColor = System.Drawing.Color.White;
            this.agRadioButton1.Location = new System.Drawing.Point(23, 143);
            this.agRadioButton1.MinimumSize = new System.Drawing.Size(22, 22);
            this.agRadioButton1.Name = "agRadioButton1";
            this.agRadioButton1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.agRadioButton1.Palette = System.Drawing.Color.MediumVioletRed;
            this.agRadioButton1.Size = new System.Drawing.Size(204, 32);
            this.agRadioButton1.TabIndex = 6;
            this.agRadioButton1.TabStop = true;
            this.agRadioButton1.Text = "agRadioButton1";
            this.agRadioButton1.UseVisualStyleBackColor = false;
            // 
            // agRadioButton2
            // 
            this.agRadioButton2.AutoSize = true;
            this.agRadioButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.agRadioButton2.BorderSize = 3F;
            this.agRadioButton2.CircleSizeMode = AgrineUI.Controls.BoxSizeMode.VeryLarge;
            this.agRadioButton2.ForeColor = System.Drawing.Color.White;
            this.agRadioButton2.Location = new System.Drawing.Point(23, 194);
            this.agRadioButton2.MinimumSize = new System.Drawing.Size(22, 22);
            this.agRadioButton2.Name = "agRadioButton2";
            this.agRadioButton2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.agRadioButton2.Palette = System.Drawing.Color.MediumVioletRed;
            this.agRadioButton2.Size = new System.Drawing.Size(204, 32);
            this.agRadioButton2.TabIndex = 6;
            this.agRadioButton2.TabStop = true;
            this.agRadioButton2.Text = "agRadioButton1";
            this.agRadioButton2.UseVisualStyleBackColor = false;
            // 
            // agRadioButton3
            // 
            this.agRadioButton3.AutoSize = true;
            this.agRadioButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.agRadioButton3.BorderSize = 3F;
            this.agRadioButton3.CircleSizeMode = AgrineUI.Controls.BoxSizeMode.VeryLarge;
            this.agRadioButton3.ForeColor = System.Drawing.Color.White;
            this.agRadioButton3.Location = new System.Drawing.Point(23, 242);
            this.agRadioButton3.MinimumSize = new System.Drawing.Size(22, 22);
            this.agRadioButton3.Name = "agRadioButton3";
            this.agRadioButton3.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.agRadioButton3.Palette = System.Drawing.Color.MediumVioletRed;
            this.agRadioButton3.Size = new System.Drawing.Size(204, 32);
            this.agRadioButton3.TabIndex = 6;
            this.agRadioButton3.TabStop = true;
            this.agRadioButton3.Text = "agRadioButton1";
            this.agRadioButton3.UseVisualStyleBackColor = false;
            // 
            // agCheckBox1
            // 
            this.agCheckBox1.AutoSize = true;
            this.agCheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.agCheckBox1.BorderSize = 2F;
            this.agCheckBox1.BoxSizeMode = AgrineUI.Controls.BoxSizeMode.Large;
            this.agCheckBox1.ForeColor = System.Drawing.Color.White;
            this.agCheckBox1.Location = new System.Drawing.Point(191, 396);
            this.agCheckBox1.MinimumSize = new System.Drawing.Size(22, 22);
            this.agCheckBox1.Name = "agCheckBox1";
            this.agCheckBox1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.agCheckBox1.Palette = System.Drawing.Color.MediumVioletRed;
            this.agCheckBox1.Radius = 7;
            this.agCheckBox1.Size = new System.Drawing.Size(179, 32);
            this.agCheckBox1.TabIndex = 7;
            this.agCheckBox1.Text = "agCheckBox1";
            this.agCheckBox1.UseVisualStyleBackColor = false;
            // 
            // agCheckBox2
            // 
            this.agCheckBox2.AutoSize = true;
            this.agCheckBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.agCheckBox2.BorderSize = 2F;
            this.agCheckBox2.BoxSizeMode = AgrineUI.Controls.BoxSizeMode.Large;
            this.agCheckBox2.ForeColor = System.Drawing.Color.White;
            this.agCheckBox2.Location = new System.Drawing.Point(191, 448);
            this.agCheckBox2.MinimumSize = new System.Drawing.Size(22, 22);
            this.agCheckBox2.Name = "agCheckBox2";
            this.agCheckBox2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.agCheckBox2.Palette = System.Drawing.Color.MediumVioletRed;
            this.agCheckBox2.Radius = 7;
            this.agCheckBox2.Size = new System.Drawing.Size(179, 32);
            this.agCheckBox2.TabIndex = 7;
            this.agCheckBox2.Text = "agCheckBox1";
            this.agCheckBox2.UseVisualStyleBackColor = false;
            // 
            // agSwitchButton1
            // 
            this.agSwitchButton1.BorderSize = 1.7F;
            this.agSwitchButton1.DarkMode = true;
            this.agSwitchButton1.Location = new System.Drawing.Point(576, 110);
            this.agSwitchButton1.MinimumSize = new System.Drawing.Size(45, 22);
            this.agSwitchButton1.Name = "agSwitchButton1";
            this.agSwitchButton1.Palette = System.Drawing.Color.Crimson;
            this.agSwitchButton1.Size = new System.Drawing.Size(56, 29);
            this.agSwitchButton1.TabIndex = 11;
            this.agSwitchButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(835, 667);
            this.Controls.Add(this.agSwitchButton1);
            this.Controls.Add(this.agCheckBox2);
            this.Controls.Add(this.agCheckBox1);
            this.Controls.Add(this.agRadioButton3);
            this.Controls.Add(this.agRadioButton2);
            this.Controls.Add(this.agRadioButton1);
            this.Controls.Add(this.agCircle1);
            this.Controls.Add(this.switchButton1);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton1);
            this.Controls.Add(this.buttonX1);
            this.DarkMode = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Palette = System.Drawing.Color.MediumVioletRed;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private AgrineUI.Controls.AGButton agButton1;
        private AgrineUI.Controls.AGButton agButton2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton1;
        private AgrineUI.Shapes.AGCircle agCircle1;
        private AgrineUI.Controls.AGRadioButton agRadioButton1;
        private AgrineUI.Controls.AGRadioButton agRadioButton2;
        private AgrineUI.Controls.AGRadioButton agRadioButton3;
        private AgrineUI.Controls.AGCheckBox agCheckBox1;
        private AgrineUI.Controls.AGCheckBox agCheckBox2;
        private AgrineUI.Controls.AGSwitchButton agSwitchButton1;
    }
}

