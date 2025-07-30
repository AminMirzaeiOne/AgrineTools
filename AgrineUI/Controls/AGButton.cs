using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Controls
{
    public class AGButton : DevComponents.DotNetBar.ButtonX, AgrineUI.Interfaces.IAGControlBorder
    {
        public AGButton()
        {
            this.InitializeComponent();
        }

        private bool defaultButton = false;

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        [Category("Border")]
        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Tomato;

        [Category("Appearance")]
        public bool DefaultButton
        {
            get { return this.defaultButton; }
            set
            {
                this.defaultButton = value;
                if (value)
                {
                    this.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
                }
                else
                {
                    this.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = AgrineUI.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(this.BorderColor, this.BorderSize))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }

        private void InitializeComponent()
        {
            this.Font = new Font("Segoe UI Semibold", 9f);
        }
    }
}
