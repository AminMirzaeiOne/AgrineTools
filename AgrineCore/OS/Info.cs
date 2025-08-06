using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AgrineCore.OS
{
    public static class Info
    {
        #region Basic Info

        public static string GetOSVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        public static string GetUserName()
        {
            return Environment.UserName;
        }

        public static string GetSystemDirectory()
        {
            return Environment.SystemDirectory;
        }

        public static string GetProcessorArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "x64" : "x86";
        }

        #endregion

        #region CPU Info

        public static string GetCPUInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select Name, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed from Win32_Processor"))
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        string name = item["Name"]?.ToString() ?? "Unknown";
                        string cores = item["NumberOfCores"]?.ToString() ?? "0";
                        string logical = item["NumberOfLogicalProcessors"]?.ToString() ?? "0";
                        string speed = item["MaxClockSpeed"]?.ToString() ?? "0";

                        return $"{name.Trim()} | Cores: {cores}, Logical Processors: {logical}, Max Clock Speed: {speed} MHz";
                    }
                }
            }
            catch { }
            return "Unknown CPU";
        }

        #endregion

        #region RAM Info

        public static ulong GetTotalRAM()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        ulong kb = (ulong)item["TotalVisibleMemorySize"];
                        return kb / 1024; // MB
                    }
                }
            }
            catch { }
            return 0;
        }

        public static ulong GetFreeRAM()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select FreePhysicalMemory from Win32_OperatingSystem"))
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        ulong kb = (ulong)item["FreePhysicalMemory"];
                        return kb / 1024; // MB
                    }
                }
            }
            catch { }
            return 0;
        }

        public static ulong GetUsedRAM()
        {
            ulong total = GetTotalRAM();
            ulong free = GetFreeRAM();
            if (total >= free) return total - free;
            return 0;
        }

        #endregion

        #region Disk Info

        public class DiskInfo
        {
            public string Name { get; set; } = "";
            public string DriveType { get; set; } = "";
            public double TotalSizeGB { get; set; }
            public double FreeSpaceGB { get; set; }

            public override string ToString()
            {
                return $"{Name} ({DriveType}) - Total: {TotalSizeGB:F2} GB, Free: {FreeSpaceGB:F2} GB";
            }
        }

        public static List<DiskInfo> GetDisksInfo()
        {
            var list = new List<DiskInfo>();
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        list.Add(new DiskInfo
                        {
                            Name = drive.Name,
                            DriveType = drive.DriveType.ToString(),
                            TotalSizeGB = drive.TotalSize / (1024.0 * 1024 * 1024),
                            FreeSpaceGB = drive.TotalFreeSpace / (1024.0 * 1024 * 1024)
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        #endregion

        #region GPU Info

        public static List<string> GetGPUs()
        {
            var list = new List<string>();
            try
            {
                using (var searcher = new ManagementObjectSearcher("select Name from Win32_VideoController"))
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        string name = item["Name"]?.ToString() ?? "Unknown GPU";
                        list.Add(name.Trim());
                    }
                }
            }
            catch { }
            return list;
        }

        #endregion

        #region Battery Info

        public static string GetBatteryStatus()
        {
            try
            {
                var powerStatus = System.Windows.Forms.SystemInformation.PowerStatus;
                int percent = (int)(powerStatus.BatteryLifePercent * 100);
                string charging = powerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online ? "Charging" : "Not Charging";
                string batteryLife = powerStatus.BatteryLifeRemaining == -1 ? "Unknown" : $"{powerStatus.BatteryLifeRemaining / 60} min remaining";

                return $"Battery: {percent}%, {charging}, {batteryLife}";
            }
            catch { }
            return "Battery info unavailable";
        }

        #endregion

        #region System Uptime

        public static TimeSpan GetSystemUptime()
        {
            try
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
            catch { }
            return TimeSpan.Zero;
        }

        #endregion

        #region Misc

        public static int GetProcessorCount()
        {
            return Environment.ProcessorCount;
        }

        public static bool Is64BitOperatingSystem()
        {
            return Environment.Is64BitOperatingSystem;
        }

        #endregion
    }
}
