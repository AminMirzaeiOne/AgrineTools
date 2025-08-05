using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AgrineUI.Controls.Advanced
{
    [ToolboxItem(true)]
    public class AGCircleButton : AgrineUI.Abstracts.AGButtonBase
    {

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw Circle Border
            using (Pen borderPen = new Pen(Palette, BorderSize))
            {
                borderPen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawEllipse(borderPen, BorderSize / 2f, BorderSize / 2f, Width - BorderSize, Height - BorderSize);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int size = Math.Min(this.Width, this.Height);
            this.Size = new Size(size, size); 

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, this.Width, this.Height);
                this.Region = new Region(path);
            }

            this.Invalidate();
        }
    }
}
