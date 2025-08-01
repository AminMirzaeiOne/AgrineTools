using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AgrineUI.Controls
{
    public class AGSwitchButton : System.Windows.Forms.Control
    {

        private bool isChecked = false;

        [Category("Behavior")]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                Invalidate(); // Redraw
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Behavior")]
        public event EventHandler CheckedChanged;

        public AGSwitchButton()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(60, 30);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = !Checked;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            using (SolidBrush backgroundBrush = new SolidBrush(this.BackColor))
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = Height / 2;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
                path.AddArc(0, Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                e.Graphics.FillPath(backgroundBrush, path);
            }

        }
    }



}
