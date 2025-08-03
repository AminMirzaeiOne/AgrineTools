using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AgrineUI.Controls.Foundation
{
    public enum VerticalTextAlignment
    {
        Top,
        Center,
        Bottom
    }

    public enum HorizontalTextAlignment
    {
        Left,
        Center,
        Right
    }

    public class AGRichTextBox : RichTextBox
    {
        private VerticalTextAlignment verticalAlign = VerticalTextAlignment.Center;
        private HorizontalTextAlignment horizontalAlign = HorizontalTextAlignment.Right;

        private const int EM_SETRECT = 0xB3;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref RECT lParam);

        [Category("Layout")]
        public VerticalTextAlignment VerticalAlignment
        {
            get { return verticalAlign; }
            set { verticalAlign = value; AlignText(); }
        }

        [Category("Layout")]
        public HorizontalTextAlignment HorizontalAlignment
        {
            get { return horizontalAlign; }
            set { horizontalAlign = value; AlignText(); }
        }

        private Color selectionBackColor = Color.Crimson;

        [Category("Appearance")]
        [Description("Custom selection background color.")]
        public Color AGSelectionBackColor
        {
            get { return selectionBackColor; }
            set { selectionBackColor = value; this.Invalidate(); }
        }


        public AGRichTextBox()
        {
            this.Font = new Font("Tahoma", 14);
            this.AcceptsTab = true;
            this.ShortcutsEnabled = true;
            this.WordWrap = true;
            this.ScrollBars = RichTextBoxScrollBars.None;
            this.SetStyle(ControlStyles.UserPaint, false); // اجازه بده Windows متن رو بکشه
            this.HideSelection = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Focused && this.SelectionLength > 0)
            {
                DrawCustomSelection(e.Graphics);
            }
        }


        private void DrawCustomSelection(Graphics g)
        {
            int start = this.SelectionStart;
            int length = this.SelectionLength;

            for (int i = 0; i < length; i++)
            {
                this.Select(start + i, 1);
                Point pos = this.GetPositionFromCharIndex(start + i);
                Size size = TextRenderer.MeasureText(this.Text.Substring(start + i, 1), this.Font);

                Rectangle highlightRect = new Rectangle(pos, size);
                using (SolidBrush brush = new SolidBrush(this.selectionBackColor))
                {
                    g.FillRectangle(brush, highlightRect);
                }
            }

            // برگردوندن انتخاب قبلی
            this.Select(start, length);
        }


        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AlignText();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AlignText();
        }

        private void AlignText()
        {
            if (!this.IsHandleCreated)
                return;

            // محاسبه ارتفاع متن برای vertical alignment
            Size textSize = TextRenderer.MeasureText(this.Text + " ", this.Font, this.ClientSize, TextFormatFlags.WordBreak);
            int paddingTop = 0;

            switch (verticalAlign)
            {
                case VerticalTextAlignment.Top:
                    paddingTop = 0;
                    break;
                case VerticalTextAlignment.Center:
                    paddingTop = Math.Max((this.ClientSize.Height - textSize.Height) / 2, 0);
                    break;
                case VerticalTextAlignment.Bottom:
                    paddingTop = Math.Max(this.ClientSize.Height - textSize.Height, 0);
                    break;
            }

            RECT rect = new RECT
            {
                Left = 4,
                Top = paddingTop,
                Right = this.ClientSize.Width - 4,
                Bottom = this.ClientSize.Height
            };

            SendMessage(this.Handle, EM_SETRECT, IntPtr.Zero, ref rect);

            // 🛠 حفظ وضعیت مکان‌نما
            int savedSelectionStart = this.SelectionStart;
            int savedSelectionLength = this.SelectionLength;

            // تنظیم ترازبندی بدون SelectAll()
            this.Select(savedSelectionStart, 0); // اطمینان از valid بودن SelectionAlignment
            switch (horizontalAlign)
            {
                case HorizontalTextAlignment.Left:
                    this.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
                    break;
                case HorizontalTextAlignment.Center:
                    this.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
                    break;
                case HorizontalTextAlignment.Right:
                    this.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
                    break;
            }

            // بازگردانی موقعیت انتخاب
            this.Select(savedSelectionStart, savedSelectionLength);
        }

    }


}

