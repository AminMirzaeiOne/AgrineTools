using Microsoft.Win32;
using System;

namespace AgrineCore.Practical
{
    public static class RegistryManager
    {
        public static object ReadValue(RegistryHive hive, string subKey, string valueName)
        {
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
                using (var key = baseKey.OpenSubKey(subKey))
                {
                    if (key == null) return null;
                    return key.GetValue(valueName);
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool WriteValue(RegistryHive hive, string subKey, string valueName, object value)
        {
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
                using (var key = baseKey.CreateSubKey(subKey))
                {
                    if (key == null) return false;
                    key.SetValue(valueName, value);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteValue(RegistryHive hive, string subKey, string valueName)
        {
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
                using (var key = baseKey.OpenSubKey(subKey, true))
                {
                    if (key == null) return false;
                    key.DeleteValue(valueName);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
