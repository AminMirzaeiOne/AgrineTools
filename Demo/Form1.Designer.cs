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
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.switchButton1 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.checkBoxX2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
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
            this.agButton1.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.agButton1.BorderRadius = ((byte)(40));
            this.agButton1.BorderSize = ((byte)(3));
            this.agButton1.DefaultButton = true;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton1.Location = new System.Drawing.Point(339, 17);
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
            this.agButton2.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.agButton2.BorderRadius = ((byte)(40));
            this.agButton2.BorderSize = ((byte)(3));
            this.agButton2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.agButton2.DefaultButton = false;
            this.agButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton2.Location = new System.Drawing.Point(339, 196);
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
            this.textBoxX1.Location = new System.Drawing.Point(180, 434);
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
            this.textBoxX2.Location = new System.Drawing.Point(180, 489);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.PreventEnterBeep = true;
            this.textBoxX2.Size = new System.Drawing.Size(233, 34);
            this.textBoxX2.TabIndex = 2;
            this.textBoxX2.WatermarkText = "Enter Text";
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX1.CheckSignSize = new System.Drawing.Size(20, 20);
            this.checkBoxX1.Location = new System.Drawing.Point(359, 370);
            this.checkBoxX1.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxX1.Size = new System.Drawing.Size(302, 58);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 3;
            this.checkBoxX1.Text = " برسی کانکشن ";
            // 
            // switchButton1
            // 
            // 
            // 
            // 
            this.switchButton1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton1.Location = new System.Drawing.Point(121, 329);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.OffText = "خاموش";
            this.switchButton1.OnText = "روشن";
            this.switchButton1.Size = new System.Drawing.Size(102, 33);
            this.switchButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton1.TabIndex = 4;
            // 
            // checkBoxX2
            // 
            // 
            // 
            // 
            this.checkBoxX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX2.CheckSignSize = new System.Drawing.Size(20, 20);
            this.checkBoxX2.Location = new System.Drawing.Point(452, 448);
            this.checkBoxX2.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxX2.Name = "checkBoxX2";
            this.checkBoxX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxX2.Size = new System.Drawing.Size(209, 58);
            this.checkBoxX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX2.TabIndex = 3;
            this.checkBoxX2.Text = " برسی کانکشن ";
            // 
            // checkBoxX3
            // 
            // 
            // 
            // 
            this.checkBoxX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX3.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX3.CheckSignSize = new System.Drawing.Size(20, 20);
            this.checkBoxX3.Location = new System.Drawing.Point(471, 526);
            this.checkBoxX3.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxX3.Name = "checkBoxX3";
            this.checkBoxX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxX3.Size = new System.Drawing.Size(190, 58);
            this.checkBoxX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX3.TabIndex = 3;
            this.checkBoxX3.Text = " برسی کانکشن ";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(697, 626);
            this.Controls.Add(this.switchButton1);
            this.Controls.Add(this.checkBoxX3);
            this.Controls.Add(this.checkBoxX2);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton1);
            this.Controls.Add(this.buttonX1);
            this.DarkMode = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Palette = System.Drawing.Color.MediumSeaGreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private AgrineUI.Controls.AGButton agButton1;
        private AgrineUI.Controls.AGButton agButton2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX3;
    }
}

