using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Practical.Actions
{
    public static class Windows
    {
        public enum OperationTypes
        {
            None, Shutdown, Restart, Sleep, Lockup, WiFiOn, WiFiOff, BluetoothOn, BluetoothOff, VolumeUp, VolumeDown, BrightnessUp, BrightnesDown
        }

        private static AgrineUI.Practical.Actions.Windows.OperationTypes operation = OperationTypes.None;

        public static AgrineUI.Practical.Actions.Windows.OperationTypes Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                switch (value)
                {
                    case OperationTypes.Shutdown:
                        AgrineUI.Practical.Actions.Windows.Shutdown();
                        break;

                    case OperationTypes.Restart:
                        AgrineUI.Practical.Actions.Windows.Restart();
                        break;

                    case OperationTypes.Sleep:
                        AgrineUI.Practical.Actions.Windows.Sleep();
                        break;

                    case OperationTypes.Lockup:
                        AgrineUI.Practical.Actions.Windows.Lockup();
                        break;
                }
            }
        }

        /// <summary>
        /// Windows operating system shutdown action
        /// </summary>
        /// <param name="delay">Windows shutdown delay value (Based on sec)</param>
        public static void Shutdown(byte delay = 5)
        {
            Process.Start($"shutdown", "/s /t " + delay);
        }

        /// <summary>
        /// Windows operating system restart action
        /// </summary>
        /// <param name="delay">Windows restart delay value (Based on sec)</param>
        public static void Restart(byte delay = 5)
        {
            Process.Start($"shutdown", "/r /t " + delay);
        }

        /// <summary>
        /// Windows operating system sleep action
        /// </summary>
        /// <param name="delay">Windows sleep delay value (Based on sec)</param>
        public static async Task Sleep(byte delay = 5)
        {
            await Task.Delay(delay * 1000);
            Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
        }

        /// <summary>
        /// Windows operating system Lockup action
        /// </summary>
        /// <param name="delay">Windows lockup delay value (Based on sec)</param>
        public static async Task Lockup(byte delay = 5)
        {
            await Task.Delay(delay * 1000);
            Process.Start("rundll32.exe", "user32.dll,LockWorkStation");
        }

        public static async Task<bool> CheckPing(string hostname = "8.8.8.8")
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(hostname, 3000); // تایم‌اوت ۳ ثانیه
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckWiFiConnection()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var ni in interfaces)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    ni.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<bool> CheckInternetConnection()
        {
            return await CheckPing("8.8.8.8");
        }

        public static bool CheckBluetoothConnection()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%Bluetooth%'"))
                {
                    foreach (var device in searcher.Get())
                    {
                        var status = device["Status"]?.ToString();
                        if (status == "OK")
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" خطا در برسی وضعیت بلوتوث سیستم" +  ex.Message);
            }

            return false;
        }





    }
}
