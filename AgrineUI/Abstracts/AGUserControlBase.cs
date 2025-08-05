using AgrineUI.Interfaces;
using DevComponents.WinForms.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Abstracts
{
    [ToolboxItem(false)]
    public abstract class AGUserControlBase : System.Windows.Forms.UserControl, IAGUserControlBorder, IAGControlTheme
    {
        private bool darkMode = false;
        private AgrineUI.Abstracts.AGUserControlBase.SpecializedRadius customRadius = new SpecializedRadius();

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        [Category("Border")]
        public bool BorderRadiusCustom { get; set; } = false;

        [Category("Border")]
        [Description("Cordner Radius User Control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SpecializedRadius CustomRadius
        {
            get { return this.customRadius; }
            set
            {
                this.customRadius = value;
                this.Invalidate();
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

            }
        }

        [Category("Theme")]
        public Color Palette { get; set; } = Color.Tomato;

        public AGUserControlBase()
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
        }



        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class SpecializedRadius
        {
            public byte TopLeft { get; set; } = 2;
            public byte TopRight { get; set; } = 2;
            public byte BottomLeft { get; set; } = 2;
            public byte BottomRight { get; set; } = 2;

            public override string ToString()
            {
                return $"TL={TopLeft}, TR={TopRight}, BL={BottomLeft}, BR={BottomRight}";
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw border
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            if (!this.BorderRadiusCustom)
                using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
                {
                    this.Region = new Region(GraphPath);
                    if (this.BorderSize > 0)
                    {
                        using (Pen pen = new Pen(this.Palette, this.BorderSize))
                        {
                            pen.Alignment = PenAlignment.Inset;
                            e.Graphics.DrawPath(pen, GraphPath);
                        }
                    }

                }
            else
                using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.CustomRadius.TopLeft, this.CustomRadius.TopRight, this.CustomRadius.BottomRight, this.CustomRadius.BottomLeft))
                {
                    this.Region = new Region(GraphPath);
                    if (this.BorderSize > 0)
                    {
                        using (Pen pen = new Pen(this.Palette, this.BorderSize))
                        {
                            pen.Alignment = PenAlignment.Inset;
                            e.Graphics.DrawPath(pen, GraphPath);
                        }
                    }

                }

        }
    }
}
