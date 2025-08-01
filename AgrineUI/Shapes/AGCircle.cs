using AgrineUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Shapes
{
    public partial class AGCircle : UserControl, AgrineUI.Interfaces.IAGShape
    {
        public AGCircle()
        {
            InitializeComponent();
        }

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Tomato;
        public bool AgreeTheme { get; set; } = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                int diameter = Math.Min(Width, Height);
                e.Graphics.FillEllipse(brush, 0, 0, diameter - 1, diameter - 1);
            }

            using (Pen pen = new Pen(this.BorderColor, this.BorderSize))
            {
                int diameter = Math.Min(Width, Height);
                e.Graphics.DrawEllipse(pen, 0, 0, diameter - 1, diameter - 1);
            }
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = Math.Min(Width, Height);
                path.AddEllipse(0, 0, diameter, diameter);
                this.Region = new Region(path);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            int size = Math.Min(Width, Height);
            this.Width = size;
            this.Height = size;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, size, size);
                this.Region = new Region(path);
            }

            this.Invalidate();

        }


    }
}
