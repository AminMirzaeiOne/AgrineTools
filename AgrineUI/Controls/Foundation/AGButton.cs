using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace AgrineUI.Controls.Foundation
{
    [ToolboxItem(true)]
    public class AGButton : AgrineUI.Abstracts.AGButtonBase, AgrineUI.Interfaces.IAGControlBorder
    {

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(this.Palette, this.BorderSize))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }


    }
}
