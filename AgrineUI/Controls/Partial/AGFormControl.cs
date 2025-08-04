using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Controls.Partial
{
    [ToolboxItem(true)]
    public class AGFormControl : AgrineUI.Controls.Advanced.AGUserControl
    {
        private Foundation.AGButton agButton1;
        private Foundation.AGButton agButton2;
        private Foundation.AGButton agButton3;

        public AGFormControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.agButton1 = new AgrineUI.Controls.Foundation.AGButton();
            this.agButton2 = new AgrineUI.Controls.Foundation.AGButton();
            this.agButton3 = new AgrineUI.Controls.Foundation.AGButton();
            this.SuspendLayout();
            // 
            // agButton1
            // 
            this.agButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton1.BorderRadius = ((byte)(20));
            this.agButton1.BorderSize = ((byte)(2));
            this.agButton1.DarkMode = true;
            this.agButton1.DefaultButton = false;
            this.agButton1.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton1.FlatAppearance.BorderSize = 0;
            this.agButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton1.Font = new System.Drawing.Font("Segoe Fluent Icons", 7F, System.Drawing.FontStyle.Bold);
            this.agButton1.ForeColor = System.Drawing.Color.White;
            this.agButton1.Location = new System.Drawing.Point(652, 8);
            this.agButton1.Name = "agButton1";
            this.agButton1.Palette = System.Drawing.Color.Tomato;
            this.agButton1.Size = new System.Drawing.Size(30, 28);
            this.agButton1.TabIndex = 0;
            this.agButton1.Text = "";
            this.agButton1.UseVisualStyleBackColor = false;
            // 
            // agButton2
            // 
            this.agButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton2.BorderRadius = ((byte)(20));
            this.agButton2.BorderSize = ((byte)(2));
            this.agButton2.DarkMode = true;
            this.agButton2.DefaultButton = false;
            this.agButton2.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton2.FlatAppearance.BorderSize = 0;
            this.agButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton2.Font = new System.Drawing.Font("Segoe Fluent Icons", 7F, System.Drawing.FontStyle.Bold);
            this.agButton2.ForeColor = System.Drawing.Color.White;
            this.agButton2.Location = new System.Drawing.Point(616, 8);
            this.agButton2.Name = "agButton2";
            this.agButton2.Palette = System.Drawing.Color.Tomato;
            this.agButton2.Size = new System.Drawing.Size(30, 28);
            this.agButton2.TabIndex = 0;
            this.agButton2.Text = "";
            this.agButton2.UseVisualStyleBackColor = false;
            // 
            // agButton3
            // 
            this.agButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton3.BorderRadius = ((byte)(20));
            this.agButton3.BorderSize = ((byte)(2));
            this.agButton3.DarkMode = true;
            this.agButton3.DefaultButton = false;
            this.agButton3.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.agButton3.FlatAppearance.BorderSize = 0;
            this.agButton3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.agButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agButton3.Font = new System.Drawing.Font("Segoe Fluent Icons", 7F, System.Drawing.FontStyle.Bold);
            this.agButton3.ForeColor = System.Drawing.Color.White;
            this.agButton3.Location = new System.Drawing.Point(580, 8);
            this.agButton3.Name = "agButton3";
            this.agButton3.Palette = System.Drawing.Color.Tomato;
            this.agButton3.Size = new System.Drawing.Size(30, 28);
            this.agButton3.TabIndex = 0;
            this.agButton3.Text = "";
            this.agButton3.UseVisualStyleBackColor = false;
            // 
            // AGFormControl
            // 
            this.Controls.Add(this.agButton3);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton1);
            this.DarkMode = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AGFormControl";
            this.Size = new System.Drawing.Size(700, 48);
            this.ResumeLayout(false);

        }

    }
}
