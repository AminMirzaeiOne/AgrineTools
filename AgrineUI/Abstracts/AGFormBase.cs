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

namespace AgrineUI.Abstracts
{
    public abstract class AGFormBase : System.Windows.Forms.Form, IAGControlBorder, IAGControlTheme
    {

        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        [Category("Theme")]
        public bool DarkMode { get; set; } = false;

        [Category("Theme")]
        public Color Palette { get; set; } = Color.Tomato;

        public AGFormBase()
        {
            this.InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw border
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AGFormBase
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AGFormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGFormBase";
            this.ResumeLayout(false);

        }
    }
}
