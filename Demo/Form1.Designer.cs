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
            this.agRectangle1 = new AgrineUI.Shapes.AGRectangle();
            this.agCircle1 = new AgrineUI.Shapes.AGCircle();
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
            this.agButton1.BorderColor = System.Drawing.SystemColors.WindowFrame;
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
            this.agButton2.BorderColor = System.Drawing.SystemColors.WindowFrame;
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
            // agRectangle1
            // 
            this.agRectangle1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.agRectangle1.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.agRectangle1.BorderRadius = ((byte)(1));
            this.agRectangle1.BorderSize = ((byte)(5));
            this.agRectangle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agRectangle1.Location = new System.Drawing.Point(69, 424);
            this.agRectangle1.Name = "agRectangle1";
            this.agRectangle1.Size = new System.Drawing.Size(145, 146);
            this.agRectangle1.TabIndex = 2;
            // 
            // agCircle1
            // 
            this.agCircle1.BackColor = System.Drawing.Color.White;
            this.agCircle1.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.agCircle1.BorderSize = ((byte)(2));
            this.agCircle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agCircle1.Location = new System.Drawing.Point(350, 399);
            this.agCircle1.Name = "agCircle1";
            this.agCircle1.Size = new System.Drawing.Size(200, 200);
            this.agCircle1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(697, 626);
            this.Controls.Add(this.agCircle1);
            this.Controls.Add(this.agRectangle1);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton1);
            this.Controls.Add(this.buttonX1);
            this.DarkMode = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Palette = System.Drawing.Color.YellowGreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private AgrineUI.Controls.AGButton agButton1;
        private AgrineUI.Controls.AGButton agButton2;
        private AgrineUI.Shapes.AGRectangle agRectangle1;
        private AgrineUI.Shapes.AGCircle agCircle1;
    }
}

