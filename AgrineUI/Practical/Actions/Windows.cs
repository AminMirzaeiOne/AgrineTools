using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
                        AgrineUI.Practical.Actions.Windows.Base.Shutdown();
                        break;

                    case OperationTypes.Restart:
                        AgrineUI.Practical.Actions.Windows.Base.Restart();
                        break;

                    case OperationTypes.Sleep:
                        AgrineUI.Practical.Actions.Windows.Base.Sleep();
                        break;

                    case OperationTypes.Lockup:
                        AgrineUI.Practical.Actions.Windows.Base.Lockup();
                        break;
                }
            }
        }

        public struct Base
        {
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
        }

        public struct Network
        {
            /// <summary>
            /// Sends a ping to the specified host to check network reachability.
            /// </summary>
            /// <param name="hostname">The hostname or IP address to ping.</param>
            /// <returns>True if the host responds successfully; otherwise, false.</returns>
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

            /// <summary>
            /// Determines whether the Wi-Fi adapter is enabled and connected on the system.
            /// </summary>
            /// <returns>True if Wi-Fi is active and operational; otherwise, false.</returns>
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

            /// <summary>
            /// Checks if the system has an active internet connection by pinging a reliable IP address.
            /// </summary>
            /// <returns>True if the internet is accessible; otherwise, false.</returns>
            public static async Task<bool> CheckInternetConnection()
            {
                return await CheckPing("8.8.8.8");
            }

            /// <summary>
            /// Checks whether a Bluetooth device is present and enabled on the system.
            /// </summary>
            /// <returns>True if Bluetooth hardware is detected and active; otherwise, false.</returns>
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
                    MessageBox.Show(" خطا در برسی وضعیت بلوتوث سیستم" + ex.Message);
                }

                return false;
            }

            public static bool CheckAirplaneMode()
            {
                return false;
            }

        }

        public struct Sound
        {
            public static int Volume { get; set; }

            public static void VolumeUp()
            {

            }

            public static void VolumeDown()
            {

            }

            public static void Mute()
            {

            }

            public static void UnMute()
            {

            }


        }

        public struct Display
        {
            public static int Brightness { get; set; }

            public static void BrightnessUp()
            {

            }

            public static void BrightnessDown()
            {

            }

            public static void OnNightMode()
            {

            }

            public static void OffNightMode()
            {

            }
        }

        public struct Appearance
        {
            public static bool CheckDarkMode()
            {
                return false;
            }

            public static System.Drawing.Color GetColor { get; }
        }

        public struct HardwareInfo
        {
            public static string CPUModel { get; }
            public static string Technology { get; }
            public static string CoreSpeed { get; }
            public static string AmountRam { get; }
            public static string AmountRemainingRam { get; }
            public static string AmountStorage { get; }
            public static string AmountRemainingStorage { get; }
        }

        public struct SoftwareInfo
        {
            public static string Name { get; }
            public static string Version { get; }
            public static string Edition { get; }
            public static string Build { get; }
            public static string Date { get; }
        }







    }
}
