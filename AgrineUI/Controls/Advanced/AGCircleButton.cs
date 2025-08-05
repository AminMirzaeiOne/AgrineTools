using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AgrineUI.Controls.Advanced
{
    public class AGCircleButton : Button, AgrineUI.Interfaces.IAGControlTheme
    {
        public AGCircleButton()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            animationTimer = new Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;

            hoverTimer = new Timer();
            hoverTimer.Interval = 30;
            hoverTimer.Tick += HoverTimer_Tick;
        }

        private bool defaultButton = false;
        private bool darkMode = false;
        private Color palette = Color.Tomato;

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;


        [Category("Appearance")]
        public bool DefaultButton
        {
            get => this.defaultButton;
            set
            {
                this.defaultButton = value;
                this.UpdateDefaultButtonStyle();
            }
        }

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                if (value)
                {
                    this.BackColor = Color.FromArgb(30, 30, 30);
                    this.ForeColor = Color.White;
                }
                else
                {
                    this.BackColor = Color.FromArgb(230, 230, 230);
                    this.ForeColor = Color.Black;
                }
                this.UpdateDefaultButtonStyle();

            }
        }

        [Category("Theme")]
        public Color Palette
        {
            get { return this.palette; }
            set
            {
                this.palette = value;
                this.UpdateDefaultButtonStyle();
            }
        }

        // ---------- Ripple Animation Fields ----------
        private Timer animationTimer;
        private int rippleRadius = 0;
        private int rippleMaxRadius = 0;
        private Point rippleCenter;
        private bool isAnimating = false;
        private float rippleOpacity = 0.4f;

        // ---------- Hover Animation Fields ----------
        private Timer hoverTimer;
        private float hoverOpacity = 0f;
        private bool hovering = false;
        private const float MaxHoverOpacity = 0.2f;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            rippleCenter = e.Location;
            rippleRadius = 0;
            rippleMaxRadius = Math.Max(Width, Height);
            isAnimating = true;
            rippleOpacity = 0.4f;
            animationTimer.Start();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            hovering = true;
            hoverTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hovering = false;
            hoverTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            rippleRadius += 10;
            rippleOpacity -= 0.02f;
            if (rippleRadius >= rippleMaxRadius || rippleOpacity <= 0)
            {
                animationTimer.Stop();
                isAnimating = false;
            }
            this.Invalidate();
        }

        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            if (hovering)
            {
                if (hoverOpacity < MaxHoverOpacity)
                    hoverOpacity += 0.03f; // سریع‌تر و قوی‌تر
                else
                    hoverTimer.Stop();
            }
            else
            {
                if (hoverOpacity > 0f)
                    hoverOpacity -= 0.03f;
                else
                    hoverTimer.Stop();
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw Fluent hover layer (before ripple)
            // Draw Fluent hover layer (before ripple)
            if (hoverOpacity > 0)
            {
                Color hoverColor;

                if (DefaultButton)
                {
                    // روشن‌تر از رنگ اصلی (قابل مشاهده‌تر)
                    hoverColor = ControlPaint.LightLight(Palette);
                }
                else
                {
                    // رنگ اصلی برای هاور
                    hoverColor = ControlPaint.Light(Palette, 0.2f);
                }

                using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb((int)(hoverOpacity * 255), hoverColor)))
                {
                    e.Graphics.FillRectangle(hoverBrush, this.ClientRectangle);
                }
            }



            // Draw Circle Border
            using (Pen borderPen = new Pen(Palette, BorderSize))
            {
                borderPen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawEllipse(borderPen, BorderSize / 2f, BorderSize / 2f, Width - BorderSize, Height - BorderSize);
            }


            // Draw ripple
            if (isAnimating)
            {
                using (SolidBrush rippleBrush = new SolidBrush(Color.FromArgb((int)(rippleOpacity * 255), Color.White)))
                {
                    e.Graphics.FillEllipse(rippleBrush,
                        rippleCenter.X - rippleRadius / 2,
                        rippleCenter.Y - rippleRadius / 2,
                        rippleRadius, rippleRadius);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int size = Math.Min(this.Width, this.Height);
            this.Size = new Size(size, size); // حفظ نسبت مربع برای دایره

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, this.Width, this.Height);
                this.Region = new Region(path);
            }

            this.Invalidate();
        }


        private void UpdateDefaultButtonStyle()
        {
            if (defaultButton)
            {
                this.BackColor = this.Palette;

                // تعیین رنگ فونت مناسب بسته به روشنایی رنگ
                double luminance = 0.2126 * Palette.R + 0.7152 * Palette.G + 0.0722 * Palette.B;
                this.ForeColor = luminance < 128 ? Color.White : Color.Black;
            }
            else
            {
                if (darkMode)
                {
                    this.BackColor = Color.FromArgb(30, 30, 30);
                    this.ForeColor = Color.White;
                }
                else
                {
                    this.BackColor = Color.FromArgb(230, 230, 230);
                    this.ForeColor = Color.Black;
                }
            }
            this.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, this.BackColor);
        }


        private void InitializeComponent()
        {
            this.Font = new Font("Segoe UI Semibold", 9f);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = this.Palette;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(40, 40);
        }
    }
}
