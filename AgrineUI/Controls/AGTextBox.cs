using AgrineUI.Interfaces;
using DevComponents.DotNetBar;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using System.Windows.Forms;

namespace AgrineUI.Controls
{
    public class AGTextBox : UserControl, IAGControlBorder
    {
        private AgrineUI.Animations.AGAnimator agAnimator1;
        private IContainer components;
        private Label label1;
        private AgrineUI.Controls.AGButton agButton1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private AGRichTextBox richtext;
        private LabelItem _2289;


        public AGTextBox()
        {
            this.InitializeComponent();
        }

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        [Category("Border")]
        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Tomato;

        [Category("Placeholder")]
        public string HolderText { get { return this.label1.Text; } set { this.label1.Text = value; } }

        [Category("Placeholder")]
        public Color HolderColor { get { return this.label1.ForeColor; } set { this.label1.ForeColor = value; } }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.richtext.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.richtext.ForeColor = value;
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                this.richtext.Font = value;
            }
        }

        [DefaultValue("")]
        [Category("Symbol")]
        [Description("Indicates the symbol displayed on face of the tile instead of the image. Setting the symbol overrides the image setting.")]
        public string Symbol
        {
            get
            {
                return this.labelX1.Symbol;
            }
            set
            {
                this.labelX1.Symbol = value;
            }
        }

        [Category("Symbol")]
        public Color SymbolColor
        {
            get { return this.labelX1.SymbolColor; }
            set { this.labelX1.SymbolColor = value; }
        }

        [Category("Symbol")]
        public float SymbolSize
        {
            get { return this.labelX1.SymbolSize; }
            set { this.labelX1.SymbolSize = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
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
            this.components = new System.ComponentModel.Container();
            AgrineUI.Animations.AGAnimation agAnimation1 = new AgrineUI.Animations.AGAnimation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AGTextBox));
            this.richtext = new AgrineUI.Controls.AGRichTextBox();
            this.agAnimator1 = new AgrineUI.Animations.AGAnimator(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.agButton1 = new AgrineUI.Controls.AGButton();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // richtext
            // 
            this.richtext.AcceptsTab = true;
            this.richtext.AGSelectionBackColor = System.Drawing.Color.Crimson;
            this.richtext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richtext.BackColor = System.Drawing.Color.White;
            this.richtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.agAnimator1.SetDecoration(this.richtext, AgrineUI.Animations.DecorationType.None);
            this.richtext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtext.HideSelection = false;
            this.richtext.HorizontalAlignment = AgrineUI.Controls.HorizontalTextAlignment.Right;
            this.richtext.Location = new System.Drawing.Point(6, 4);
            this.richtext.Name = "richtext";
            this.richtext.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.richtext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richtext.Size = new System.Drawing.Size(284, 46);
            this.richtext.TabIndex = 0;
            this.richtext.Text = "";
            this.richtext.VerticalAlignment = AgrineUI.Controls.VerticalTextAlignment.Center;
            this.richtext.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // agAnimator1
            // 
            this.agAnimator1.AnimationType = AgrineUI.Animations.AnimationType.Transparent;
            this.agAnimator1.Cursor = null;
            agAnimation1.AnimateOnlyDifferences = true;
            agAnimation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("agAnimation1.BlindCoeff")));
            agAnimation1.LeafCoeff = 0F;
            agAnimation1.MaxTime = 1F;
            agAnimation1.MinTime = 0F;
            agAnimation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("agAnimation1.MosaicCoeff")));
            agAnimation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("agAnimation1.MosaicShift")));
            agAnimation1.MosaicSize = 0;
            agAnimation1.Padding = new System.Windows.Forms.Padding(0);
            agAnimation1.RotateCoeff = 0F;
            agAnimation1.RotateLimit = 0F;
            agAnimation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("agAnimation1.ScaleCoeff")));
            agAnimation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("agAnimation1.SlideCoeff")));
            agAnimation1.TimeCoeff = 0F;
            agAnimation1.TransparencyCoeff = 1F;
            this.agAnimator1.DefaultAnimation = agAnimation1;
            this.agAnimator1.MaxAnimationTime = 1000;
            this.agAnimator1.TimeStep = 0.04F;
            this.agAnimator1.Upside = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.agAnimator1.SetDecoration(this.label1, AgrineUI.Animations.DecorationType.None);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(171, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "متن وارد کنید";
            // 
            // agButton1
            // 
            this.agButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.agButton1.BorderColor = System.Drawing.Color.Transparent;
            this.agButton1.BorderRadius = ((byte)(10));
            this.agButton1.BorderSize = ((byte)(0));
            this.agAnimator1.SetDecoration(this.agButton1, AgrineUI.Animations.DecorationType.None);
            this.agButton1.DefaultButton = true;
            this.agButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.agButton1.Location = new System.Drawing.Point(9, 15);
            this.agButton1.Name = "agButton1";
            this.agButton1.Size = new System.Drawing.Size(25, 25);
            this.agButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.agButton1.Symbol = "";
            this.agButton1.SymbolSize = 9F;
            this.agButton1.TabIndex = 3;
            this.agButton1.Tooltip = "پاک کردن متن";
            this.agButton1.Visible = false;
            this.agButton1.Click += new System.EventHandler(this.agButton1_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.agAnimator1.SetDecoration(this.labelX1, AgrineUI.Animations.DecorationType.None);
            this.labelX1.Location = new System.Drawing.Point(291, 17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(21, 23);
            this.labelX1.Symbol = "";
            this.labelX1.SymbolColor = System.Drawing.Color.Crimson;
            this.labelX1.SymbolSize = 9F;
            this.labelX1.TabIndex = 0;
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // AGTextBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.agButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richtext);
            this.agAnimator1.SetDecoration(this, AgrineUI.Animations.DecorationType.None);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AGTextBox";
            this.Size = new System.Drawing.Size(320, 54);
            this.SizeChanged += new System.EventHandler(this.AGTextBox_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.richtext.Text != string.Empty)
            {
                this.agAnimator1.Hide(this.label1);
                this.agButton1.Visible = true;
            }
            else
            {
                this.agAnimator1.Show(this.label1);
                this.agButton1.Visible = false;
            }
        }

        private void agButton1_Click(object sender, EventArgs e)
        {
            this.richtext.Text = string.Empty;
        }

        private void AGTextBox_SizeChanged(object sender, EventArgs e)
        {
            this.label1.Location = new Point(this.label1.Location.X, (this.richtext.Size.Height / 2) - 10);
            this.agButton1.Location = new Point(this.agButton1.Location.X, (this.richtext.Size.Height / 2) - 8);
            this.labelX1.Location = new Point(this.labelX1.Location.X, (this.richtext.Size.Height / 2) - 8);
        }
    }

}
