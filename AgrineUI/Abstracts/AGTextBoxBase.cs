using AgrineUI.Controls;
using AgrineUI.Interfaces;
using DevComponents.DotNetBar;
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

    public class AGTextBoxBase : Control, IAGControlTheme
    {
        private Color palette = Color.Tomato;
        private Color focusColor = Color.FromArgb(180, Color.Tomato);
        private bool darkMode = false;
        private byte borderSize = 3;

        private List<string> _pages = new List<string> { "" };
        private int _currentPageIndex = 0;
        private int _cursorIndex = 0;

        private AGHorizontalAlignment _horizontalAlignment = AGHorizontalAlignment.Left;
        private AGVerticalAlignment _verticalAlignment = AGVerticalAlignment.Top;

        private Timer _cursorTimer;
        private bool _showCursor = true;

        private int _selectionStart = -1;
        private int _selectionEnd = -1;

        private Stack<TextState> _undoStack = new Stack<TextState>();
        private Stack<TextState> _redoStack = new Stack<TextState>();



        private TextOrientation _orientation = TextOrientation.Horizontal;

        [Category("Appearance")]
        [Description("جهت نمایش متن در کنترل.")]
        public TextOrientation Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                Invalidate(); // برای بازپینت
            }
        }

        [Category("Border")]
        public byte BorderSize
        {
            get { return this.borderSize; }
            set
            {
                this.borderSize = value;
            }
        }

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        // Properties

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                if (value)
                {
                    this.ForeColor = Color.White;
                    this.BackColor = Color.FromArgb(40, 40, 40);
                }
                else
                {
                    this.ForeColor = Color.Black;
                    this.BackColor = Color.FromArgb(245,245,245);
                }
            }
        }

        [Category("Theme")]
        public Color Palette
        {
            get { return this.palette; }
            set
            {
                this.palette = value;
                this.focusColor = Color.FromArgb(180, value);
            }
        }



        public enum AGHorizontalAlignment
        {
            Left,
            Center,
            Right
        }

        public enum AGVerticalAlignment
        {
            Top,
            Center,
            Bottom
        }

        public enum TextOrientation
        {
            Horizontal,
            Vertical
        }




        public AGTextBoxBase()
        {
            this.Cursor = Cursors.IBeam;
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.Selectable, true);

            BackColor = Color.White;
            ForeColor = Color.Black;
            Font = new Font("Segoe UI", 10);

            _cursorTimer = new Timer();
            _cursorTimer.Interval = 500;
            _cursorTimer.Tick += (s, e) =>
            {
                _showCursor = !_showCursor;
                Invalidate();
            };
            _cursorTimer.Start();
        }

        [Category("Appearance")]
        [Description("تراز افقی متن در کنترل.")]
        public AGHorizontalAlignment HorizontalTextAlignment
        {
            get => _horizontalAlignment;
            set
            {
                _horizontalAlignment = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("تراز عمودی متن در کنترل.")]
        public AGVerticalAlignment VerticalTextAlignment
        {
            get => _verticalAlignment;
            set
            {
                _verticalAlignment = value;
                Invalidate();
            }
        }

        private string CurrentPage
        {
            get => _pages[_currentPageIndex];
            set => _pages[_currentPageIndex] = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);
            string text = CurrentPage;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(this.Focused ? this.Palette : this.focusColor, this.BorderSize))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }

            if (string.IsNullOrEmpty(text)) return;

            var textBrush = new SolidBrush(ForeColor);
            var selectionBrush = new SolidBrush(this.palette);

            float x = 0;
            float y = 0;

            if (_orientation == TextOrientation.Horizontal)
            {
                SizeF textSize = e.Graphics.MeasureString(text, Font);

                // محاسبه X بر اساس تراز افقی
                switch (_horizontalAlignment)
                {
                    case AGHorizontalAlignment.Left:
                        x = 2;
                        break;
                    case AGHorizontalAlignment.Center:
                        x = (Width - textSize.Width) / 2;
                        break;
                    case AGHorizontalAlignment.Right:
                        x = Width - textSize.Width - 2;
                        break;
                    default:
                        x = 2;
                        break;
                }


                // محاسبه Y بر اساس تراز عمودی
                switch (_verticalAlignment)
                {
                    case AGVerticalAlignment.Top:
                        y = 2;
                        break;
                    case AGVerticalAlignment.Center:
                        y = (Height - textSize.Height) / 2;
                        break;
                    case AGVerticalAlignment.Bottom:
                        y = Height - textSize.Height - 2;
                        break;
                    default:
                        y = 2;
                        break;
                }


                // رسم انتخاب
                if (HasSelection)
                {
                    int totalIndex = 0;
                    for (int i = 0; i < _currentPageIndex; i++)
                        totalIndex += _pages[i].Length;

                    int pageStart = totalIndex;
                    int pageEnd = pageStart + text.Length;

                    int selStart = Math.Max(_selectionStart, pageStart);
                    int selEnd = Math.Min(_selectionEnd, pageEnd);

                    if (selStart < selEnd)
                    {
                        int localStart = selStart - pageStart;
                        int localEnd = selEnd - pageStart;

                        string before = text.Substring(0, localStart);
                        string selected = text.Substring(localStart, localEnd - localStart);

                        SizeF beforeSize = e.Graphics.MeasureString(before, Font);
                        SizeF selectedSize = e.Graphics.MeasureString(selected, Font);

                        RectangleF selectionRect = new RectangleF(
                            x + beforeSize.Width,
                            y,
                            selectedSize.Width,
                            Font.Height);

                        e.Graphics.FillRectangle(selectionBrush, selectionRect);
                    }
                }

                // رسم متن
                e.Graphics.DrawString(text, Font, textBrush, new PointF(x, y));

                // رسم مکان‌نما
                if (Focused && _showCursor)
                {
                    string beforeCursor = text.Substring(0, _cursorIndex);
                    SizeF beforeSize = e.Graphics.MeasureString(beforeCursor, Font);

                    float cx = x + beforeSize.Width - 1;
                    float cy = y;
                    e.Graphics.DrawLine(Pens.Black, cx, cy, cx, cy + Font.Height);
                }
            }
            else // حالت عمودی
            {
                float totalHeight = text.Length * Font.Height;

                // محاسبه X (تراز افقی)
                switch (_horizontalAlignment)
                {
                    case AGHorizontalAlignment.Left:
                        x = 2;
                        break;
                    case AGHorizontalAlignment.Center:
                        x = (Width - Font.SizeInPoints) / 2;
                        break;
                    case AGHorizontalAlignment.Right:
                        x = Width - Font.SizeInPoints - 2;
                        break;
                    default:
                        x = 2;
                        break;
                }


                // محاسبه Y (تراز عمودی)
                switch (_verticalAlignment)
                {
                    case AGVerticalAlignment.Top:
                        y = 2;
                        break;
                    case AGVerticalAlignment.Center:
                        y = (Height - totalHeight) / 2;
                        break;
                    case AGVerticalAlignment.Bottom:
                        y = Height - totalHeight - 2;
                        break;
                    default:
                        y = 2;
                        break;
                }


                for (int i = 0; i < text.Length; i++)
                {
                    float cy = y + i * Font.Height;

                    // بررسی Selection
                    int totalIndex = _pages.Take(_currentPageIndex).Sum(p => p.Length);
                    int absIndex = totalIndex + i;

                    bool isSelected = HasSelection &&
                        absIndex >= Math.Min(_selectionStart, _selectionEnd) &&
                        absIndex < Math.Max(_selectionStart, _selectionEnd);

                    if (isSelected)
                    {
                        RectangleF selRect = new RectangleF(x, cy, Font.Height, Font.Height);
                        e.Graphics.FillRectangle(selectionBrush, selRect);
                    }

                    e.Graphics.DrawString(text[i].ToString(), Font, textBrush, x, cy);
                }

                // مکان‌نما
                if (Focused && _showCursor && _cursorIndex <= text.Length)
                {
                    float cy = y + _cursorIndex * Font.Height;
                    e.Graphics.DrawLine(Pens.Black, x, cy, x + Font.Height, cy);
                }
            }


        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            _cursorIndex = GetCharIndexFromPosition(e.Location);
            _selectionStart = _cursorIndex;
            _selectionEnd = _cursorIndex;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                _selectionEnd = GetCharIndexFromPosition(e.Location);
                _cursorIndex = _selectionEnd;
                Invalidate();
            }
        }


        protected override bool IsInputKey(Keys keyData) => true;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            SaveStateForUndo();


            if (!char.IsControl(e.KeyChar))
            {
                string newText = CurrentPage.Insert(_cursorIndex, e.KeyChar.ToString());

                int fitChars = MeasureFittableCharCount(newText);
                if (newText.Length > fitChars)
                {
                    // رفتن به صفحه جدید
                    _pages.Insert(_currentPageIndex + 1, newText.Substring(fitChars));
                    CurrentPage = newText.Substring(0, fitChars);
                    _currentPageIndex++;
                    _cursorIndex = 0;
                }
                else
                {
                    CurrentPage = newText;
                    _cursorIndex++;
                }

                Invalidate();
            }

            if (char.IsControl(e.KeyChar)) return;

            SaveStateForUndo();

            if (HasSelection)
            {
                DeleteSelectedText(); // حذف انتخاب‌شده
            }

            InsertTextAtCursor(e.KeyChar.ToString());
        }



        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (_cursorIndex > 0)
                        _cursorIndex--;
                    else if (_currentPageIndex > 0)
                    {
                        _currentPageIndex--;
                        _cursorIndex = CurrentPage.Length;
                    }
                    break;

                case Keys.Right:
                    if (_cursorIndex < CurrentPage.Length)
                        _cursorIndex++;
                    else if (_currentPageIndex < _pages.Count - 1)
                    {
                        _currentPageIndex++;
                        _cursorIndex = 0;
                    }
                    break;

                case Keys.Back:
                    if (HasSelection)
                    {
                        SaveStateForUndo();
                        // حذف متن انتخاب‌شده از تمام صفحات
                        DeleteSelectedText();
                    }
                    else if (_cursorIndex > 0)
                    {
                        if (_cursorIndex <= CurrentPage.Length)
                        {
                            CurrentPage = CurrentPage.Remove(_cursorIndex - 1, 1);
                            _cursorIndex--;
                        }
                        else
                        {
                            _cursorIndex = CurrentPage.Length;
                        }
                    }
                    else if (_currentPageIndex > 0)
                    {
                        // برگشت به صفحه قبل
                        string prevPage = _pages[_currentPageIndex - 1];
                        _cursorIndex = prevPage.Length;
                        _pages[_currentPageIndex - 1] += CurrentPage;
                        _pages.RemoveAt(_currentPageIndex);
                        _currentPageIndex--;
                    }

                    Invalidate();
                    e.Handled = true;
                    break;


            }

            bool isShift = (ModifierKeys & Keys.Shift) == Keys.Shift;

            if (e.KeyCode == Keys.Left)
            {
                if (_cursorIndex > 0)
                    _cursorIndex--;

                if (isShift)
                {
                    if (_selectionStart == -1)
                        _selectionStart = _cursorIndex + 1;

                    _selectionEnd = _cursorIndex;
                }
                else
                {
                    ClearSelection();
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_cursorIndex < CurrentPage.Length)
                    _cursorIndex++;

                if (isShift)
                {
                    if (_selectionStart == -1)
                        _selectionStart = _cursorIndex - 1;

                    _selectionEnd = _cursorIndex;
                }
                else
                {
                    ClearSelection();
                }

                e.Handled = true;
            }


            Invalidate();
        }

        private int MeasureFittableCharCount(string text)
        {
            Graphics g = CreateGraphics();
            int maxWidth = ClientSize.Width - 4;
            for (int i = 1; i <= text.Length; i++)
            {
                string substr = text.Substring(0, i);
                var size = g.MeasureString(substr, Font);
                if (size.Width > maxWidth)
                    return i - 1;
            }
            return text.Length;
        }

        private void Copy()
        {
            if (HasSelection)
            {
                string selectedText = GetSelectedText();
                Clipboard.SetText(selectedText);
            }
            else if (!string.IsNullOrEmpty(CurrentPage))
            {
                Clipboard.SetText(CurrentPage);
            }
        }


        private void Paste()
        {
            if (Clipboard.ContainsText())
            {
                string pasteText = Clipboard.GetText();

                if (HasSelection)
                {
                    DeleteSelectedText();
                }

                InsertTextAtCursor(pasteText);
            }
        }


        private void Cut()
        {
            if (HasSelection)
            {
                string selectedText = GetSelectedText();
                Clipboard.SetText(selectedText);
                DeleteSelectedText();
            }
            else if (!string.IsNullOrEmpty(CurrentPage))
            {
                Clipboard.SetText(CurrentPage);
                CurrentPage = string.Empty;
                _cursorIndex = 0;
                Invalidate();
            }
        }


        private void SelectAll()
        {
            _currentPageIndex = 0;
            _cursorIndex = 0;

            _selectionStart = 0;

            // مجموع کاراکترها تا آخرین صفحه
            int totalLength = 0;
            foreach (var page in _pages)
                totalLength += page.Length;

            _selectionEnd = totalLength;

            Invalidate();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.C:
                        SaveStateForUndo();
                        this.Copy();
                        return true;

                    case Keys.Control | Keys.V:
                        SaveStateForUndo();
                        this.Paste();
                        return true;

                    case Keys.Control | Keys.X:
                        SaveStateForUndo();
                        this.Cut();
                        return true;

                    case Keys.Control | Keys.A:
                        this.SelectAll();
                        return true;

                    case Keys.Control | Keys.Z:
                        Undo();
                        return true;

                    case Keys.Control | Keys.Y:
                        Redo();
                        return true;

                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool HasSelection => _selectionStart >= 0 && _selectionEnd >= 0 && _selectionStart != _selectionEnd;

        private void ClearSelection()
        {
            _selectionStart = -1;
            _selectionEnd = -1;
        }

        private int GetCharIndexFromPosition(Point point)
        {
            int x = 2;

            for (int i = 0; i < CurrentPage.Length; i++)
            {
                string ch = CurrentPage.Substring(i, 1);
                Size size = TextRenderer.MeasureText(ch, Font);

                Rectangle charRect = new Rectangle(x, 2, size.Width, size.Height);

                if (point.X < x + size.Width / 2)
                    return i;

                x += size.Width;
            }

            return CurrentPage.Length;
        }

        private void DeleteSelectedText()
        {
            int start = Math.Min(_selectionStart, _selectionEnd);
            int end = Math.Max(_selectionStart, _selectionEnd);
            int totalIndex = 0;

            for (int i = 0; i < _pages.Count;)
            {
                int pageLen = _pages[i].Length;
                int pageStart = totalIndex;
                int pageEnd = pageStart + pageLen;

                if (end <= pageStart)
                {
                    break; // Selection به این صفحه نمی‌رسد
                }
                else if (start >= pageEnd)
                {
                    totalIndex += pageLen;
                    i++;
                    continue; // Selection هنوز شروع نشده
                }
                else
                {
                    // بخشی از انتخاب در این صفحه است
                    int localStart = Math.Max(start - pageStart, 0);
                    int localEnd = Math.Min(end - pageStart, pageLen);

                    string pageText = _pages[i];
                    string before = pageText.Substring(0, localStart);
                    string after = pageText.Substring(localEnd);

                    string newText = before + after;

                    _pages[i] = newText;

                    totalIndex += newText.Length;

                    // اگر صفحه خالی شد، حذفش کن (به جز اگر تنها صفحه باقی‌مانده است)
                    if (string.IsNullOrEmpty(_pages[i]) && _pages.Count > 1)
                    {
                        _pages.RemoveAt(i);
                        continue; // دوباره به همین اندیس نگاه کن
                    }

                    i++;
                }
            }

            _cursorIndex = start;

            // پیدا کردن صفحه جدید برای مکان‌نما
            int runningLength = 0;
            for (int i = 0; i < _pages.Count; i++)
            {
                int len = _pages[i].Length;
                if (start <= runningLength + len)
                {
                    _currentPageIndex = i;
                    _cursorIndex = start - runningLength;
                    break;
                }
                runningLength += len;
            }

            ClearSelection();
        }



        private string GetSelectedText()
        {
            int start = Math.Min(_selectionStart, _selectionEnd);
            int end = Math.Max(_selectionStart, _selectionEnd);
            int totalIndex = 0;
            string result = "";

            foreach (var page in _pages)
            {
                int pageLen = page.Length;
                int pageStart = totalIndex;
                int pageEnd = pageStart + pageLen;

                if (end <= pageStart)
                    break;

                if (start >= pageEnd)
                {
                    totalIndex += pageLen;
                    continue;
                }

                int localStart = Math.Max(start - pageStart, 0);
                int localEnd = Math.Min(end - pageStart, pageLen);

                result += page.Substring(localStart, localEnd - localStart);
                totalIndex += pageLen;
            }

            return result;
        }


        private void InsertTextAtCursor(string text)
        {
            // ادغام متن paste شده با متن فعلی
            string current = CurrentPage;
            string newText = current.Insert(_cursorIndex, text);

            // حذف صفحه فعلی
            _pages.RemoveAt(_currentPageIndex);

            // تقسیم متن جدید به صفحات
            List<string> newPages = SplitTextIntoPages(newText);

            // درج صفحات جدید به جای صفحه قبلی
            _pages.InsertRange(_currentPageIndex, newPages);

            // محاسبه مکان نهایی مکان‌نما
            int cursorAbsIndex = 0;
            for (int i = 0; i < _currentPageIndex; i++)
                cursorAbsIndex += _pages[i].Length;

            cursorAbsIndex += _cursorIndex + text.Length;

            // پیدا کردن صفحه و موقعیت مکان‌نما
            int total = 0;
            for (int i = 0; i < _pages.Count; i++)
            {
                int len = _pages[i].Length;
                if (cursorAbsIndex <= total + len)
                {
                    _currentPageIndex = i;
                    _cursorIndex = cursorAbsIndex - total;
                    break;
                }
                total += len;
            }

            ClearSelection();
            Invalidate();
        }


        private List<string> SplitTextIntoPages(string fullText)
        {
            List<string> result = new List<string>();

            int index = 0;
            while (index < fullText.Length)
            {
                int maxChars = MeasureFittableCharCount(fullText.Substring(index));
                if (maxChars == 0)
                    break;

                result.Add(fullText.Substring(index, maxChars));
                index += maxChars;
            }

            if (result.Count == 0)
                result.Add(""); // در صورت خالی بودن

            return result;
        }

        private void SaveStateForUndo()
        {
            _undoStack.Push(CaptureState());
            _redoStack.Clear(); // هر زمان کاربر تایپ جدیدی کرد، redo پاک می‌شود
        }

        private TextState CaptureState()
        {
            return new TextState
            {
                Pages = new List<string>(_pages),
                CursorIndex = _cursorIndex,
                PageIndex = _currentPageIndex,
                SelectionStart = _selectionStart,
                SelectionEnd = _selectionEnd
            };
        }

        private void RestoreState(TextState state)
        {
            _pages = new List<string>(state.Pages);
            _cursorIndex = state.CursorIndex;
            _currentPageIndex = state.PageIndex;
            _selectionStart = state.SelectionStart;
            _selectionEnd = state.SelectionEnd;
            Invalidate();
        }

        private void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(CaptureState());
                var state = _undoStack.Pop();
                RestoreState(state);
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(CaptureState());
                var state = _redoStack.Pop();
                RestoreState(state);
            }
        }




        private class TextState
        {
            public List<string> Pages { get; set; }
            public int CursorIndex { get; set; }
            public int PageIndex { get; set; }
            public int SelectionStart { get; set; }
            public int SelectionEnd { get; set; }

            public TextState Clone()
            {
                return new TextState
                {
                    Pages = new List<string>(Pages),
                    CursorIndex = CursorIndex,
                    PageIndex = PageIndex,
                    SelectionStart = SelectionStart,
                    SelectionEnd = SelectionEnd
                };
            }
        }


    }

}
