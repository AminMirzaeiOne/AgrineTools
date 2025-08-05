using AgrineUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Abstracts
{
    [ToolboxItem(false)]
    public abstract class AGButtonBase : System.Windows.Forms.Button, IAGControlTheme, IAGControlAnimation, IAGControlSound
    {
        public AGButtonBase()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            animationTimer = new Timer();
            animationTimer.Interval = 14;
            animationTimer.Tick += AnimationTimer_Tick;

            hoverTimer = new Timer();
            hoverTimer.Interval = 30;
            hoverTimer.Tick += HoverTimer_Tick;

            this.buttonSounds = new Data.Constants.Sound.Buttons();
            this.soundPlayer = new SoundPlayer(buttonSounds.ASoundClick);

        }

        private bool defaultButton = false;
        private bool darkMode = false;
        private Color palette = Color.Tomato;

        private SoundPlayer soundPlayer;
        private AgrineUI.Data.Constants.Sound.Buttons buttonSounds;
        private AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions animationSpeed = AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions.Medium;


        [Category("Sound")]
        public bool Sound { get; set; } = true;

        [Category("Animation")]
        public bool Animation { get; set; } = true;

        [Category("Animation")]
        public AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions AnimationSpeed
        {
            get { return this.animationSpeed; }
            set
            {
                this.animationSpeed = value;
                switch (value)
                {
                    case Data.Enums.AnimationControls.AnimationSpeedOptions.VerySlow:
                        this.animationTimer.Interval = AgrineUI.Data.Constants.Animation.Button.ClickInterval_VerySlow;
                        this.hoverTimer.Interval = AgrineUI.Data.Constants.Animation.Button.HoverInterval_VerySlow;
                        break;

                    case Data.Enums.AnimationControls.AnimationSpeedOptions.Slow:
                        this.animationTimer.Interval = AgrineUI.Data.Constants.Animation.Button.ClickInterval_Slow;
                        this.hoverTimer.Interval = AgrineUI.Data.Constants.Animation.Button.HoverInterval_Slow;
                        break;

                    case Data.Enums.AnimationControls.AnimationSpeedOptions.Medium:
                        this.animationTimer.Interval = AgrineUI.Data.Constants.Animation.Button.ClickInterval_Medium;
                        this.hoverTimer.Interval = AgrineUI.Data.Constants.Animation.Button.HoverInterval_Medium;
                        break;

                    case Data.Enums.AnimationControls.AnimationSpeedOptions.Quick:
                        this.animationTimer.Interval = AgrineUI.Data.Constants.Animation.Button.ClickInterval_Quick;
                        this.hoverTimer.Interval = AgrineUI.Data.Constants.Animation.Button.HoverInterval_Quick;
                        break;

                    case Data.Enums.AnimationControls.AnimationSpeedOptions.VeryQuick:
                        this.animationTimer.Interval = AgrineUI.Data.Constants.Animation.Button.ClickInterval_VeryQuick;
                        this.hoverTimer.Interval = AgrineUI.Data.Constants.Animation.Button.HoverInterval_VeryQuick;
                        break;

                }
            }
        }



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

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this.Sound)
                this.soundPlayer.Play();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.Animation)
            {
                rippleCenter = e.Location;
                rippleRadius = 0;
                rippleMaxRadius = Math.Max(Width, Height);
                isAnimating = true;
                rippleOpacity = 0.4f;
                animationTimer.Start();
            }

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.Animation)
            {
                hovering = true;
                hoverTimer.Start();
            }

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.Animation)
            {
                hovering = false;
                hoverTimer.Start();
            }

        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (this.Animation)
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

        }

        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            if (this.Animation)
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
                    this.BackColor = AgrineUI.Data.Constants.Theme.Buttons.DarkBackColor;
                    this.ForeColor = AgrineUI.Data.Constants.Theme.Buttons.DarkForeColor;
                }
                else
                {
                    this.BackColor = AgrineUI.Data.Constants.Theme.Buttons.LightBackColor;
                    this.ForeColor = AgrineUI.Data.Constants.Theme.Buttons.LightForeColor;
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
        }
    }
}
