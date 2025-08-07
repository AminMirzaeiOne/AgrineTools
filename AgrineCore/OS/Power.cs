using System;
using System.Runtime.InteropServices;
using System.Management;

namespace AgrineCore.OS
{
 

    public static class Power
    {
        #region P/Invoke برای کنترل حالت‌های برق

        [DllImport("kernel32.dll")]
        private static extern bool SetSystemPowerState(bool fSuspend, bool fForce);

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern uint SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        [DllImport("user32.dll")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        // Flags for ExitWindowsEx
        private const uint EWX_LOGOFF = 0x00000000;
        private const uint EWX_SHUTDOWN = 0x00000001;
        private const uint EWX_REBOOT = 0x00000002;
        private const uint EWX_FORCE = 0x00000004;
        private const uint EWX_POWEROFF = 0x00000008;

        #endregion

        #region Power Status Info

        public static bool IsBatteryPresent()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery"))
                {
                    return searcher.Get().Count > 0;
                }
            }
            catch { return false; }
        }

        public static int GetBatteryLifePercent()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery"))
                {
                    foreach (var mo in searcher.Get())
                    {
                        var val = mo["EstimatedChargeRemaining"];
                        if (val != null)
                            return Convert.ToInt32(val);
                    }
                }
                return -1;
            }
            catch { return -1; }
        }

        public static bool IsCharging()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery"))
                {
                    foreach (var mo in searcher.Get())
                    {
                        var status = mo["BatteryStatus"];
                        if (status != null)
                        {
                            int st = Convert.ToInt32(status);
                            // Status codes 6,7 = charging
                            return st == 6 || st == 7;
                        }
                    }
                }
                return false;
            }
            catch { return false; }
        }

        #endregion

        #region Power Actions

        public static bool Sleep()
        {
            try
            {
                uint result = SetSuspendState(false, true, true);
                return result == 0; // 0 usually means success
            }
            catch { return false; }
        }

        public static bool Hibernate()
        {
            try
            {
                uint result = SetSuspendState(true, true, true);
                return result == 0;
            }
            catch { return false; }
        }


        public static bool LogOff()
        {
            try
            {
                return ExitWindowsEx(EWX_LOGOFF, 0);
            }
            catch { return false; }
        }

        public static bool Shutdown(bool force = false)
        {
            try
            {
                return ExitWindowsEx(force ? (EWX_SHUTDOWN | EWX_FORCE) : EWX_SHUTDOWN, 0);
            }
            catch { return false; }
        }

        public static bool Reboot(bool force = false)
        {
            try
            {
                return ExitWindowsEx(force ? (EWX_REBOOT | EWX_FORCE) : EWX_REBOOT, 0);
            }
            catch { return false; }
        }

        #endregion

        #region Brightness Control (Windows 8+)

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int SetDeviceGammaRamp(IntPtr hdc, ref RAMP lpRamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Blue;
        }

        public static bool SetBrightness(int brightnessPercent)
        {
            try
            {
                if (brightnessPercent < 0 || brightnessPercent > 100)
                    throw new ArgumentOutOfRangeException("brightnessPercent", "Value must be between 0 and 100");

                IntPtr hdc = GetDC(IntPtr.Zero);
                if (hdc == IntPtr.Zero)
                    return false;

                RAMP ramp = new RAMP();
                ramp.Red = new ushort[256];
                ramp.Green = new ushort[256];
                ramp.Blue = new ushort[256];

                for (int i = 0; i < 256; i++)
                {
                    int val = i * brightnessPercent / 100;
                    ushort val16 = (ushort)(val * 256);
                    ramp.Red[i] = ramp.Green[i] = ramp.Blue[i] = val16;
                }

                bool result = SetDeviceGammaRamp(hdc, ref ramp) != 0;
                ReleaseDC(IntPtr.Zero, hdc);
                return result;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }

}
