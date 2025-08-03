using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Controls.Advanced
{
    public class AGToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        // آیکون Symbol (با فونت Symbol یا FontAwesome یا Segoe MDL2 Assets)
        private string _symbolText = "\uE10F"; // پیش‌فرض: آیکون مثال از Segoe MDL2
        [Category("AG Custom")]
        public string SymbolText
        {
            get => _symbolText;
            set { _symbolText = value; Invalidate(); }
        }

        // فونت آیکون Symbol
        private Font _symbolFont = new Font("Segoe MDL2 Assets", 12);
        [Category("AG Custom")]
        public Font SymbolFont
        {
            get => _symbolFont;
            set { _symbolFont = value; Invalidate(); }
        }

        // رنگ آیکون
        private Color _symbolColor = Color.Gray;
        [Category("AG Custom")]
        public Color SymbolColor
        {
            get => _symbolColor;
            set { _symbolColor = value; Invalidate(); }
        }

        // گردی گوشه‌ها
        private int _cornerRadius = 6;
        [Category("AG Custom")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        // حالت دارک مود
        private bool _darkMode = false;
        [Category("AG Custom")]
        public bool DarkMode
        {
            get => _darkMode;
            set { _darkMode = value; Invalidate(); }
        }

        // رنگ پس‌زمینه
        private Color _baseBackColor = Color.White;
        [Category("AG Custom")]
        public Color BaseBackColor
        {
            get => _baseBackColor;
            set { _baseBackColor = value; Invalidate(); }
        }

        // رنگ متن
        private Color _baseForeColor = Color.Black;
        [Category("AG Custom")]
        public Color BaseForeColor
        {
            get => _baseForeColor;
            set { _baseForeColor = value; Invalidate(); }
        }

        public AGToolStripMenuItem()
        {
            AutoSize = false;
            Height = 32;
            Margin = new Padding(4, 2, 4, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width, Height);
            var radius = _cornerRadius;

            using (var bgBrush = new SolidBrush(_darkMode ? Color.FromArgb(30, 30, 30) : _baseBackColor))
            using (var textBrush = new SolidBrush(_darkMode ? Color.White : _baseForeColor))
            using (var symbolBrush = new SolidBrush(_symbolColor))
            {
                var path = RoundedRect(rect, radius);
                g.FillPath(bgBrush, path);

                // متن اصلی
                var textSize = g.MeasureString(Text, Font);
                float textX = 10;
                float textY = (Height - textSize.Height) / 2;
                g.DrawString(Text, Font, textBrush, textX, textY);

                // آیکون Symbol (در سمت راست متن)
                if (!string.IsNullOrEmpty(_symbolText))
                {
                    var symbolSize = g.MeasureString(_symbolText, _symbolFont);
                    float symbolX = textX + textSize.Width + 8;
                    float symbolY = (Height - symbolSize.Height) / 2;
                    g.DrawString(_symbolText, _symbolFont, symbolBrush, symbolX, symbolY);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // انیمیشن کلیک ساده: تغییر رنگ لحظه‌ای
            _baseBackColor = Color.FromArgb(180, _baseBackColor);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            // برگرداندن رنگ به حالت اولیه
            _baseBackColor = DarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
            Invalidate();
        }

        // تابع برای رسم گوشه گرد
        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
