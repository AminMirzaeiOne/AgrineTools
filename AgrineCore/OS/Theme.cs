using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace AgrineCore.OS
{
    public static class Theme
    {
        #region Windows Version Check

        public static Version GetWindowsVersion()
        {
            // Using Environment.OSVersion is deprecated but still works in many cases.
            // For precise version info in modern Windows, need manifest or API calls, 
            // but here a simple version check is enough.

            return Environment.OSVersion.Version;
        }

        public static bool IsWindows10OrGreater()
        {
            var version = GetWindowsVersion();
            return (version.Major >= 10);
        }

        #endregion

        #region Dark Mode Support and Status

        // Check if system supports dark mode (Windows 10 version 1809+)
        public static bool SupportsDarkMode()
        {
            if (!IsWindows10OrGreater())
                return false;

            // For simplicity, assume Windows 10 version 1809 (build 17763) or newer supports dark mode
            var version = GetWindowsVersion();
            return version.Build >= 17763;
        }

        // Read from registry if system theme is dark or light
        // Registry path for system theme:
        // HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize
        // Value: SystemUsesLightTheme (DWORD)
        // 0 = Dark mode, 1 = Light mode

        public static bool IsDarkModeEnabled()
        {
            if (!SupportsDarkMode())
                return false;

            try
            {
                using (RegistryKey personalizeKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (personalizeKey == null)
                        return false;

                    object val = personalizeKey.GetValue("SystemUsesLightTheme");
                    if (val == null)
                        return false;

                    int useLightTheme = (int)val;
                    return useLightTheme == 0; // 0 means dark mode enabled
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Get & Set Accent Color (Windows 10+)

        // Accent color is stored in registry (DWORD ARGB) in:
        // HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM\ColorizationColor
        // Note: Changing accent color programmatically is possible by changing registry
        // but requires notifying system or logging off to apply fully.

        public static uint GetAccentColor()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM"))
                {
                    if (key == null)
                        return 0;
                    object val = key.GetValue("ColorizationColor");
                    if (val == null)
                        return 0;

                    return (uint)(int)val;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static bool SetAccentColor(uint argbColor)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM", true))
                {
                    if (key == null)
                        return false;
                    key.SetValue("ColorizationColor", (int)argbColor, RegistryValueKind.DWord);
                }
                // To fully apply changes, broadcasting WM_SETTINGCHANGE or restart might be needed.
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Change System Theme (Light/Dark)

        // Changing the system theme via code means changing:
        // "AppsUseLightTheme" and "SystemUsesLightTheme" values under
        // HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize
        // 0 = dark mode, 1 = light mode

        public static bool SetDarkModeEnabled(bool enableDark)
        {
            if (!SupportsDarkMode())
                return false;

            try
            {
                using (RegistryKey personalizeKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
                {
                    if (personalizeKey == null)
                        return false;

                    int val = enableDark ? 0 : 1;
                    personalizeKey.SetValue("SystemUsesLightTheme", val, RegistryValueKind.DWord);
                    personalizeKey.SetValue("AppsUseLightTheme", val, RegistryValueKind.DWord);

                    // To apply the change immediately, might need to broadcast WM_SETTINGCHANGE or restart apps.
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

