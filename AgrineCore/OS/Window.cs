using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace AgrineCore.OS
{
    public static class Window
    {
        #region Win32 API

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9;

        // Import Win32 API functions needed for screen capture
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
            IntPtr hdcSrc, int xSrc, int ySrc, CopyPixelOperation rop);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        #endregion

        /// <summary>
        /// Captures screenshot of the window identified by its handle.
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <returns>Bitmap image of the window screenshot</returns>
        public static Bitmap CaptureWindowScreenshot(IntPtr hWnd)
        {
            // Step 1: Get window rectangle size
            GetWindowRect(hWnd, out RECT rect);
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            // Step 2: Get the window's device context (DC)
            IntPtr hWindowDC = GetWindowDC(hWnd);

            // Step 3: Create a compatible DC in memory
            IntPtr hMemoryDC = CreateCompatibleDC(hWindowDC);

            // Step 4: Create a compatible bitmap for the memory DC
            IntPtr hBitmap = CreateCompatibleBitmap(hWindowDC, width, height);

            // Step 5: Select the bitmap into the memory DC
            IntPtr hOld = SelectObject(hMemoryDC, hBitmap);

            // Step 6: Copy window contents from window DC to memory DC (bit-block transfer)
            BitBlt(hMemoryDC, 0, 0, width, height, hWindowDC, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

            // Step 7: Restore original bitmap object
            SelectObject(hMemoryDC, hOld);

            // Step 8: Clean up DCs
            DeleteDC(hMemoryDC);
            ReleaseDC(hWnd, hWindowDC);

            // Step 9: Create Bitmap object from the handle
            Bitmap bmp = Image.FromHbitmap(hBitmap);

            // Step 10: Delete the GDI bitmap handle to avoid resource leak
            DeleteObject(hBitmap);

            // Return the captured bitmap
            return bmp;
        }

        /// <summary>
        /// Finds the handle of a window by exact title match
        /// </summary>
        public static IntPtr FindWindowByTitle(string windowTitle)
        {
            return FindWindow(null, windowTitle);
        }

        /// <summary>
        /// Finds all windows containing the given substring in their title
        /// </summary>
        public static List<IntPtr> FindWindowsByTitleContains(string partialTitle)
        {
            var windows = new List<IntPtr>();

            EnumWindows((hWnd, lParam) =>
            {
                int length = GetWindowTextLength(hWnd);
                if (length == 0)
                    return true; // continue enumerating

                StringBuilder builder = new StringBuilder(length + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                string title = builder.ToString();

                if (title.IndexOf(partialTitle, StringComparison.OrdinalIgnoreCase) >= 0 && IsWindowVisible(hWnd))
                {
                    windows.Add(hWnd);
                }

                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary>
        /// Sets focus to the window
        /// </summary>
        public static bool SetFocus(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Shows or hides a window
        /// </summary>
        public static bool Show(IntPtr hWnd, bool show)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return ShowWindow(hWnd, show ? SW_SHOW : SW_HIDE);
        }

        /// <summary>
        /// Minimizes a window
        /// </summary>
        public static bool Minimize(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return ShowWindow(hWnd, SW_MINIMIZE);
        }

        /// <summary>
        /// Maximizes a window
        /// </summary>
        public static bool Maximize(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return ShowWindow(hWnd, SW_MAXIMIZE);
        }

        /// <summary>
        /// Restores a minimized or maximized window
        /// </summary>
        public static bool Restore(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return ShowWindow(hWnd, SW_RESTORE);
        }

        /// <summary>
        /// Moves and resizes a window
        /// </summary>
        public static bool Move(IntPtr hWnd, int x, int y, int width, int height, bool repaint = true)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return MoveWindow(hWnd, x, y, width, height, repaint);
        }

        /// <summary>
        /// Gets window rectangle (position and size)
        /// </summary>
        public static Rectangle? GetWindowRectangle(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return null;

            RECT rect;
            if (GetWindowRect(hWnd, out rect))
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            return null;
        }

        /// <summary>
        /// Closes a window gracefully (sends WM_CLOSE)
        /// </summary>
        public static bool Close(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return CloseWindow(hWnd);
        }

        /// <summary>
        /// Destroys a window immediately (use with caution)
        /// </summary>
        public static bool Destroy(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            return DestroyWindow(hWnd);
        }
    }
}
