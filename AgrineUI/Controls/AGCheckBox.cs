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
    public enum BoxSizeMode
    {
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    public class AGCheckBox : CheckBox
    {
        private Color palette = Color.Tomato;
        private float borderSize = 2f;
        private int radius = 4;
        private BoxSizeMode boxSizeMode = BoxSizeMode.Medium;

        private float animProgress = 0f;
        private Timer animationTimer;
        private const float animStep = 1.5f;

        [Category("Appearance")]
        public Color Palette
        {
            get => this.palette;
            set { this.palette = value; Invalidate(); }
        }

        [Category("Appearance")]
        public BoxSizeMode BoxSizeMode
        {
            get => boxSizeMode;
            set { boxSizeMode = value; Invalidate(); }
        }

        [Category("Border Style")]
        public float BorderSize
        {
            get => this.borderSize;
            set { this.borderSize = value; Invalidate(); }
        }

        [Category("Border Style")]
        public int Radius
        {
            get => this.radius;
            set { this.radius = Math.Max(0, value); Invalidate(); }
        }

        

        public AGCheckBox()
        {
            this.MinimumSize = new Size(22, 22);
            this.AutoSize = false;
            this.Padding = new Padding(24, 0, 0, 0);
            this.SetStyle(ControlStyles.UserPaint, true);

            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimationTimer_Tick;

            this.CheckedChanged += AGCheckBox_CheckedChanged;
        }

        private void AGCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                animProgress = 0;
                animationTimer.Start();
            }
            else
            {
                Invalidate();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animProgress += animStep;
            if (animProgress >= GetBoxSize())
            {
                animProgress = GetBoxSize();
                animationTimer.Stop();
            }
            this.Invalidate();
        }

        private float GetBoxSize()
        {
            switch (this.BoxSizeMode)
            {
                case BoxSizeMode.VerySmall: return 10f;
                case BoxSizeMode.Small: return 14f;
                case BoxSizeMode.Medium: return 18f;
                case BoxSizeMode.Large: return 22f;
                case BoxSizeMode.VeryLarge: return 26f;
                default: return 18f;
            }
        }

        private GraphicsPath GetRoundedRect(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;

            if (radius <= 0f)
            {
                path.AddRectangle(rect);
            }
            else
            {
                RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
                path.AddArc(arc, 180, 90); 

                arc.X = rect.Right - diameter;
                path.AddArc(arc, 270, 90);

                arc.Y = rect.Bottom - diameter;
                path.AddArc(arc, 0, 90);

                arc.X = rect.X;
                path.AddArc(arc, 90, 90);

                path.CloseFigure();
            }

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent?.BackColor ?? this.BackColor);

            float boxSize = GetBoxSize();
            float offset = this.BorderSize / 2f;

            RectangleF boxRect = new RectangleF(
                offset,
                (Height - boxSize) / 2f,
                boxSize,
                boxSize
            );

            using (GraphicsPath borderPath = GetRoundedRect(boxRect, this.Radius))
            using (Pen borderPen = new Pen(this.Palette, this.BorderSize))
            {
                pevent.Graphics.DrawPath(borderPen, borderPath);
            }

            if (this.Checked)
            {
                if (animProgress >= boxSize)
                {
                    using (Brush fillBrush = new SolidBrush(this.Palette))
                    using (GraphicsPath fillPath = GetRoundedRect(boxRect, this.Radius))
                    {
                        pevent.Graphics.FillPath(fillBrush, fillPath);
                    }

                    Color tickColor = IsColorDark(this.Palette) ? Color.White : Color.Black;

                    using (Pen tickPen = new Pen(tickColor, this.BorderSize))
                    {
                        float pad = boxSize * 0.25f;

                        PointF p1 = new PointF(boxRect.X + pad - 1, boxRect.Y + boxRect.Height / 2);
                        PointF p2 = new PointF(boxRect.X + boxRect.Width / 2 - 1, boxRect.Y + boxRect.Height - pad);
                        PointF p3 = new PointF(boxRect.X + boxRect.Width - pad, boxRect.Y + pad + 1);

                        pevent.Graphics.DrawLines(tickPen, new PointF[] { p1, p2, p3 });
                    }
                }
                else
                {
                    float grow = animProgress;
                    float shrink = (boxSize - grow) / 2f;

                    RectangleF fillRect = new RectangleF(
                        boxRect.X + shrink,
                        boxRect.Y + shrink,
                        grow,
                        grow
                    );

                    using (GraphicsPath fillPath = GetRoundedRect(fillRect, this.Radius / 1.5f))
                    using (Brush brush = new SolidBrush(this.Palette))
                    {
                        pevent.Graphics.FillPath(brush, fillPath);
                    }
                }
            }


            TextRenderer.DrawText(
                pevent.Graphics,
                this.Text,
                this.Font,
                new Point((int)(boxRect.Right + 6), (int)((Height - Font.Height) / 2f)),
                this.ForeColor
            );
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        private bool IsColorDark(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }

    }
}
