using AgrineUI.Controls.Advanced;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Controls.Foundation
{
    public class AGComboBox : ComboBox,AgrineUI.Interfaces.IAGControlTheme
    {
        private int borderRadius = 8;
        private int borderSize = 2;
        private Color palette = Color.Tomato;
        private bool darkMode = false;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetComboBoxInfo(IntPtr hwndCombo, ref COMBOBOXINFO info);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [Category("AG Appearance")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        [Category("AG Appearance")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Category("AG Appearance")]
        public Color Palette
        {
            get => this.palette;
            set { this.palette = value; Invalidate(); }
        }

        [Category("AG Appearance")]
        public bool DarkMode
        {
            get => darkMode;
            set
            {
                darkMode = value;
                ApplyTheme();
                Invalidate();
            }
        }

        public AGComboBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            ApplyTheme();
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            ApplyDropDownStyling();
        }

        private void ApplyDropDownStyling()
        {
            try
            {
                COMBOBOXINFO info = new COMBOBOXINFO();
                info.cbSize = Marshal.SizeOf(info);
                GetComboBoxInfo(this.Handle, ref info);

                IntPtr dropDownHandle = info.hwndList;

                if (dropDownHandle != IntPtr.Zero)
                {
                    // اعمال شعاع گرد
                    Rectangle rect = GetWindowRectangle(dropDownHandle);
                    IntPtr hRgn = CreateRoundRectRgn(0, 0, rect.Width, rect.Height, borderRadius * 2, borderRadius * 2);
                    SetWindowRgn(dropDownHandle, hRgn, true);
                }
            }
            catch
            {
                // اگر مشکلی بود، نادیده بگیر
            }
        }

        private Rectangle GetWindowRectangle(IntPtr hWnd)
        {
            RECT r;
            GetWindowRect(hWnd, out r);
            return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);



        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                string text = Items[e.Index].ToString();

                bool isHovered = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                Color backgroundColor = isHovered ? palette : (darkMode ? Color.FromArgb(45, 45, 48) : Color.White);
                Color textColor = darkMode ? Color.White : Color.Black;

                using (SolidBrush bgBrush = new SolidBrush(backgroundColor))
                using (SolidBrush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                    e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds);
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

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

            // متن
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                                  new Rectangle(5, 3, rect.Width - 20, rect.Height),
                                  darkMode ? Color.White : Color.Black,
                                  TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            // فلش
            DrawArrow(e.Graphics, rect);
        }

        private void ApplyTheme()
        {
            this.BackColor = darkMode ? Color.FromArgb(45, 45, 48) : Color.White;
            this.ForeColor = darkMode ? Color.White : Color.Black;
        }

        private void DrawArrow(Graphics g, Rectangle rect)
        {
            Point middle = new Point(rect.Width - 15, rect.Height / 2);
            Point[] arrow = {
            new Point(middle.X - 5, middle.Y - 2),
            new Point(middle.X + 5, middle.Y - 2),
            new Point(middle.X, middle.Y + 4)
        };

            using (Brush b = new SolidBrush(darkMode ? Color.White : Color.Black))
            {
                g.FillPolygon(b, arrow);
            }
        }

     


        

    }
}
