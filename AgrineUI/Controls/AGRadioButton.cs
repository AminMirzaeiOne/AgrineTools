using AgrineUI.Interfaces;
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
    public class AGRadioButton : RadioButton, IAGControlTheme
    {
        private Color palette = Color.Tomato;
        private bool darkMode = false;
        private float borderSize = 2f;

        private float animRadius = 0f;
        private Timer animationTimer;
        private const float maxInnerSize = 10f;
        private const float animStep = 1.5f;

        private BoxSizeMode circleSizeMode = BoxSizeMode.Medium;

        [Category("Appearance")]
        public BoxSizeMode CircleSizeMode
        {
            get => this.circleSizeMode;
            set { this.circleSizeMode = value; Invalidate(); }
        }


        [Category("Theme")]
        public Color Palette
        {
            get => palette;
            set { palette = value; Invalidate(); }
        }

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                this.BackColor = this.Parent.BackColor;
                if (value)
                {
                    this.ForeColor = Color.White;
                }
                else
                {
                    this.ForeColor = Color.Black;
                }
            }
        }

        [Category("Border Style")]
        public float BorderSize
        {
            get => this.borderSize;
            set
            {
                this.borderSize = value;
                this.Invalidate();
            }
        }

        public AGRadioButton()
        {
            this.MinimumSize = new Size(22, 22);
            this.AutoSize = false;
            this.Padding = new Padding(24, 0, 0, 0);

            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimationTimer_Tick;

            this.CheckedChanged += AGRadioButton_CheckedChanged;
        }

        private void AGRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                animRadius = 0;
                animationTimer.Start();
            }
            else
            {
                Invalidate();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animRadius += animStep;
            if (animRadius >= maxInnerSize)
            {
                animRadius = maxInnerSize;
                animationTimer.Stop();
            }
            this.Invalidate();
        }

        private float GetCircleSize()
        {
            switch (this.CircleSizeMode)
            {
                case BoxSizeMode.VerySmall: return 10f;
                case BoxSizeMode.Small: return 14f;
                case BoxSizeMode.Medium: return 18f;
                case BoxSizeMode.Large: return 22f;
                case BoxSizeMode.VeryLarge: return 26f;
                default: return 18f;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent?.BackColor ?? this.BackColor);

            float circleSize = GetCircleSize();
            float offset = this.BorderSize / 2f;

            RectangleF outerRect = new RectangleF(
                offset,
                (Height - circleSize) / 2f,
                circleSize,
                circleSize
            );

            using (Pen borderPen = new Pen(this.Palette, this.BorderSize))
            {
                pevent.Graphics.DrawEllipse(borderPen, outerRect);
            }

            if (this.Checked)
            {
                float innerSize = animRadius;
                float innerOffset = (circleSize - innerSize) / 2f;

                RectangleF innerRect = new RectangleF(
                    outerRect.X + innerOffset,
                    outerRect.Y + innerOffset,
                    innerSize,
                    innerSize
                );

                using (Brush fillBrush = new SolidBrush(this.Palette))
                {
                    pevent.Graphics.FillEllipse(fillBrush, innerRect);
                }
            }

            TextRenderer.DrawText(
                pevent.Graphics,
                this.Text,
                this.Font,
                new Point((int)(outerRect.Right + 6), (int)((Height - Font.Height) / 2f)),
                this.ForeColor
            );
        }

        private bool IsColorDark(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }





        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
    }

}
