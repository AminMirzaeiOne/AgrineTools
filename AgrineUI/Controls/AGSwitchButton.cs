using AgrineUI.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AgrineUI.Controls
{
    public class AGSwitchButton : CheckBox, IAGControlTheme
    {
        private Color onBackColor = Color.Tomato;
        private Color onToggleColor = Color.White;
        private Color offBackColor = Color.Gray;
        private Color offToggleColor = Color.WhiteSmoke;

        private bool darkMode = false;
        private float borderSize = 1.7f;

        private Timer animationTimer;
        private float animationValue = 0f;
        private const int animationInterval = 15;
        private const int animationSteps = 10;
        private float animationStepValue;
        private bool isAnimatingToChecked;

        // Properties

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                if (value)
                {
                    this.offBackColor = Color.FromArgb(60, 60, 60);
                    this.offToggleColor = Color.DarkGray;
                }
                else
                {
                    this.offBackColor = Color.Gray;
                    this.offToggleColor = Color.WhiteSmoke;
                }
            }
        }

        [Category("Theme")]
        public Color Palette
        {
            get { return this.onBackColor; }
            set
            {
                this.onBackColor = value;
            }
        }

        [Category("Border")]
        public float BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Browsable(false)]
        public override string Text
        {
            get => base.Text;
            set { }
        }


        public AGSwitchButton()
        {
            this.MinimumSize = new Size(45, 22);
            this.DoubleBuffered = true;

            animationTimer = new Timer();
            animationTimer.Interval = animationInterval;
            animationTimer.Tick += AnimationTimer_Tick;

            animationStepValue = 1f / animationSteps;
            this.CheckedChanged += (s, e) => StartAnimation(this.Checked);

            if (this.Checked)
                animationValue = 1f;
        }


        private void StartAnimation(bool toChecked)
        {
            isAnimatingToChecked = toChecked;
            animationTimer.Stop();

            if (toChecked)
                animationValue = 0f;
            else
                animationValue = 1f;

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isAnimatingToChecked)
            {
                animationValue += animationStepValue;
                if (animationValue >= 1f)
                {
                    animationValue = 1f;
                    animationTimer.Stop();
                }
            }
            else
            {
                animationValue -= animationStepValue;
                if (animationValue <= 0f)
                {
                    animationValue = 0f;
                    animationTimer.Stop();
                }
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent.BackColor);

            GraphicsPath path = GetFigurePath();



            using (SolidBrush backBrush = new SolidBrush(this.Checked ? onBackColor : offBackColor))
                pevent.Graphics.FillPath(backBrush, path);

            using (Pen borderPen = new Pen(this.onBackColor, this.BorderSize))
                pevent.Graphics.DrawPath(borderPen, path);



            float toggleXPos = 2 + (this.Width - this.Height) * animationValue;
            using (SolidBrush toggleBrush = new SolidBrush(this.Checked ? onToggleColor : offToggleColor))
            {
                pevent.Graphics.FillEllipse(toggleBrush,
                    new RectangleF(toggleXPos, 2, toggleSize, toggleSize));
            }



        }

        private GraphicsPath GetFigurePath()
        {
            int arcSize = this.Height - 1;
            Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle rightArc = new Rectangle(this.Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();

            return path;
        }
    }

}

