using AgrineUI.Controls.Advanced;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Controls.Foundation
{
    public class AGContextMenustrip : ContextMenuStrip
    {
        private bool _darkMode = false;
        [Category("AG Custom")]
        public bool DarkMode
        {
            get => _darkMode;
            set
            {
                _darkMode = value;
                ApplyTheme();
            }
        }

        private Color _itemBackColor = Color.White;
        [Category("AG Custom")]
        public Color ItemBackColor
        {
            get => _itemBackColor;
            set
            {
                _itemBackColor = value;
                ApplyTheme();
            }
        }

        private Color _itemForeColor = Color.Black;
        [Category("AG Custom")]
        public Color ItemForeColor
        {
            get => _itemForeColor;
            set
            {
                _itemForeColor = value;
                ApplyTheme();
            }
        }

        public AGContextMenustrip()
        {
            Renderer = new ToolStripProfessionalRenderer(new AGColorTable());
        }

        // افزودن آیتم جدید به صورت AGToolStripMenuItem
        public AGToolStripMenuItem AddItem(string text, string symbol = "")
        {
            var item = new AGToolStripMenuItem
            {
                Text = text,
                SymbolText = symbol,
                DarkMode = this.DarkMode,
                BaseBackColor = this.ItemBackColor,
                BaseForeColor = this.ItemForeColor
            };

            Items.Add(item);
            return item;
        }

        // اعمال تنظیمات روی تمام آیتم‌ها
        private void ApplyTheme()
        {
            foreach (ToolStripItem item in Items)
            {
                if (item is AGToolStripMenuItem agItem)
                {
                    agItem.DarkMode = this.DarkMode;
                    agItem.BaseBackColor = this.ItemBackColor;
                    agItem.BaseForeColor = this.ItemForeColor;
                }
            }

            Invalidate();
        }

        // رنگ‌های سفارشی برای کلیک و هاور (اختیاری)
        private class AGColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(100, 150, 240);
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(100, 150, 240);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(100, 150, 240);
        }
    }
}
