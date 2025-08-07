using System;
using System.Management;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AgrineCore.OS
{
    public static class Display
    {
        #region Screen Info

        public static int ScreenCount => Screen.AllScreens.Length;

        public static Rectangle PrimaryScreenBounds => Screen.PrimaryScreen.Bounds;

        public static Size PrimaryScreenResolution => Screen.PrimaryScreen.Bounds.Size;

        public static bool IsMultiMonitor => Screen.AllScreens.Length > 1;

        public static Rectangle PrimaryWorkingArea => Screen.PrimaryScreen.WorkingArea;

        public static string[] GetScreenDeviceNames()
        {
            var screens = Screen.AllScreens;
            string[] names = new string[screens.Length];
            for (int i = 0; i < screens.Length; i++)
            {
                names[i] = screens[i].DeviceName;
            }
            return names;
        }

        public static float GetCurrentDpi()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                return g.DpiX;
        }

        public static float GetSystemDpi()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int dpi = GetDeviceCaps(hdc, LOGPIXELSX);
            ReleaseDC(IntPtr.Zero, hdc);
            return dpi;
        }

        public static float GetScalePercent()
        {
            float dpi = GetSystemDpi();
            return (dpi / 96f) * 100f;
        }

        public static string GetScreenOrientation()
        {
            var size = PrimaryScreenResolution;
            return size.Width >= size.Height ? "Landscape" : "Portrait";
        }

        #endregion

        #region Resolution & Refresh Rate

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public int dmColor;
            public int dmDuplex;
            public int dmYResolution;
            public int dmTTOption;
            public int dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private const int LOGPIXELSX = 88;

        public static int GetRefreshRate()
        {
            DEVMODE vDevMode = new DEVMODE();
            vDevMode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            if (EnumDisplaySettings(null, -1, ref vDevMode))
                return vDevMode.dmDisplayFrequency;
            return -1;
        }

        public static bool SetResolution(int width, int height)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            if (EnumDisplaySettings(null, -1, ref dm))
            {
                dm.dmPelsWidth = width;
                dm.dmPelsHeight = height;
                dm.dmFields = 0x00080000 | 0x00100000;
                int result = ChangeDisplaySettings(ref dm, 0);
                return result == 0;
            }
            return false;
        }

        #endregion

        #region Brightness

        public static bool IsLaptop()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("Select * from Win32_Battery"))
                {
                    return searcher.Get().Count > 0;
                }
            }
            catch { return false; }
        }

        public static int GetBrightness()
        {
            try
            {
                using (var mclass = new ManagementClass("WmiMonitorBrightness"))
                {
                    mclass.Scope = new ManagementScope("root\\wmi");
                    foreach (ManagementObject instance in mclass.GetInstances())
                    {
                        return Convert.ToInt32(instance["CurrentBrightness"]);
                    }
                }
            }
            catch { }
            return -1;
        }

        public static bool SetBrightness(byte targetBrightness)
        {
            try
            {
                using (var mclass = new ManagementClass("WmiMonitorBrightnessMethods"))
                {
                    mclass.Scope = new ManagementScope("root\\wmi");
                    foreach (ManagementObject instance in mclass.GetInstances())
                    {
                        object[] args = new object[] { 1, targetBrightness };
                        instance.InvokeMethod("WmiSetBrightness", args);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public static bool IncreaseBrightness(byte amount)
        {
            int current = GetBrightness();
            if (current == -1) return false;
            byte newValue = (byte)Math.Min(current + amount, 100);
            return SetBrightness(newValue);
        }

        public static bool DecreaseBrightness(byte amount)
        {
            int current = GetBrightness();
            if (current == -1) return false;
            byte newValue = (byte)Math.Max(current - amount, 0);
            return SetBrightness(newValue);
        }

        #endregion
    }
}
