using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Dialogs
{
    public class AGColorDialog : AgrineUI.Forms.AGForm
    {
        public AGColorDialog()
        {
            this.InitializeComponent();
        }

        private AgrineUI.Controls.Foundation.AGButton agButton2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private AgrineUI.Controls.Foundation.AGButton agButton3;
        private DevComponents.DotNetBar.ScrollBar.HScrollBarAdv hScrollBarAdv1;
        private System.Windows.Forms.Label label1;
        private AgrineUI.Controls.Foundation.AGButton agButton1;

        private void InitializeComponent()
        {
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.agButton2 = new AgrineUI.Controls.Foundation.AGButton();
            this.agButton3 = new AgrineUI.Controls.Foundation.AGButton();
            this.agButton1 = new AgrineUI.Controls.Foundation.AGButton();
            this.hScrollBarAdv1 = new DevComponents.DotNetBar.ScrollBar.HScrollBarAdv();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(14, 13);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(477, 267);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 1;
            this.groupPanel1.Text = "رنگ های پایه";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pictureBox1.Location = new System.Drawing.Point(516, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(284, 257);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel2.Location = new System.Drawing.Point(14, 301);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(477, 214);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "رنگ های سفارشی";
            // 
            // agButton2
            // 
            this.agButton2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton2.BorderRadius = ((byte)(20));
            this.agButton2.BorderSize = ((byte)(2));
            this.agButton2.DefaultButton = false;
            this.agButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agButton2.Location = new System.Drawing.Point(520, 457);
            this.agButton2.Name = "agButton2";
            this.agButton2.Size = new System.Drawing.Size(130, 55);
            this.agButton2.TabIndex = 0;
            this.agButton2.Text = "لغو";
            // 
            // agButton3
            // 
            this.agButton3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton3.BorderRadius = ((byte)(20));
            this.agButton3.BorderSize = ((byte)(2));
            this.agButton3.DefaultButton = false;
            this.agButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton3.Location = new System.Drawing.Point(516, 319);
            this.agButton3.Name = "agButton3";
            this.agButton3.Size = new System.Drawing.Size(282, 45);
            this.agButton3.TabIndex = 0;
            this.agButton3.Text = "افزودن رنگ سفارشی";
            // 
            // agButton1
            // 
            this.agButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.agButton1.BorderRadius = ((byte)(20));
            this.agButton1.BorderSize = ((byte)(2));
            this.agButton1.DefaultButton = true;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agButton1.Location = new System.Drawing.Point(668, 457);
            this.agButton1.Name = "agButton1";
            this.agButton1.Size = new System.Drawing.Size(130, 55);
            this.agButton1.TabIndex = 0;
            this.agButton1.Text = "انتخاب";
            // 
            // hScrollBarAdv1
            // 
            this.hScrollBarAdv1.Location = new System.Drawing.Point(514, 282);
            this.hScrollBarAdv1.Name = "hScrollBarAdv1";
            this.hScrollBarAdv1.Size = new System.Drawing.Size(284, 23);
            this.hScrollBarAdv1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(520, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 57);
            this.label1.TabIndex = 4;
            this.label1.Text = "رنگ نهایی";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AGColorDialog
            // 
            this.ClientSize = new System.Drawing.Size(823, 535);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollBarAdv1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.agButton2);
            this.Controls.Add(this.agButton3);
            this.Controls.Add(this.agButton1);
            this.DarkMode = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AGColorDialog";
            this.Text = "AGColorDialog";
            this.TitleText = "AGColorDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
