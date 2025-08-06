using AgrineCore.Practical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AgrineCore.OS
{

    public static class Network
    {
        public static string WiFiAdapterName { get; set; } = "Wi-Fi";

        public static string GetIPAddress() =>
            NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
                .FirstOrDefault(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork)
                ?.Address.ToString() ?? "No IP Found";

        public static string GetHostName() => Dns.GetHostName();

        public static bool IsInternetAvailable()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }


        public static long PingHost(string host)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(host, 1000);
                    return reply.Status == IPStatus.Success ? reply.RoundtripTime : -1;
                }
            }
            catch
            {
                return -1;
            }
        }


        public static void EnableWiFi() =>
            Cmd.RunSilent($"netsh interface set interface \"{WiFiAdapterName}\" enabled", true);

        public static void DisableWiFi() =>
            Cmd.RunSilent($"netsh interface set interface \"{WiFiAdapterName}\" disabled", true);

        public static string GetInterfaces() =>
            Cmd.Run("netsh interface show interface").Output;

        public static string GetMacAddress() =>
            NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up &&
                                     n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                ?.GetPhysicalAddress()
                ?.ToString() ?? "Not Found";

        public static string GetDefaultGateway() =>
            NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                .SelectMany(ni => ni.GetIPProperties().GatewayAddresses)
                .FirstOrDefault()
                ?.Address.ToString() ?? "No Gateway Found";

        public static void ReleaseAndRenewIP()
        {
            Cmd.RunSilent("ipconfig /release", true);
            Cmd.RunSilent("ipconfig /renew", true);
        }

        public static void FlushDNS() =>
            Cmd.RunSilent("ipconfig /flushdns", true);

        public static bool IsAdapterEnabled()
        {
            var output = Cmd.Run("netsh interface show interface").Output;
            return output.Split('\n')
                .Any(line => line.Contains(WiFiAdapterName) &&
                            (line.Contains("Connected") || line.Contains("Enabled")));
        }

        public static void OpenNetworkSettings()
        {
            Runtime.Start(new ProcessStartInfo
            {
                FileName = "ms-settings:network",
                UseShellExecute = true
            });
        }

        public static string ListAvailableWiFiNetworks() =>
            Cmd.Run("netsh wlan show networks").Output;

        public static bool ConnectToWiFi(string ssid, string password)
        {
            string xml = $@"
<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">
  <name>{ssid}</name>
  <SSIDConfig><SSID><name>{ssid}</name></SSID></SSIDConfig>
  <connectionType>ESS</connectionType><connectionMode>auto</connectionMode>
  <MSM><security>
    <authEncryption><authentication>WPA2PSK</authentication><encryption>AES</encryption><useOneX>false</useOneX></authEncryption>
    <sharedKey><keyType>passPhrase</keyType><protected>false</protected><keyMaterial>{password}</keyMaterial></sharedKey>
  </security></MSM>
</WLANProfile>";

            string path = Path.Combine(Path.GetTempPath(), ssid + ".xml");
            File.WriteAllText(path, xml);

            Cmd.RunSilent($"netsh wlan add profile filename=\"{path}\"");
            var result = Cmd.Run($"netsh wlan connect name=\"{ssid}\"");
            return result.Output.Contains("completed successfully");
        }

        public static void DisconnectWiFi() =>
            Cmd.RunSilent("netsh wlan disconnect");

        public static string GetCurrentWiFiSSID()
        {
            var output = Cmd.Run("netsh wlan show interfaces").Output;
            foreach (var line in output.Split('\n'))
            {
                if (line.Trim().StartsWith("SSID") && !line.Contains("BSSID"))
                    return line.Split(':')[1].Trim();
            }
            return "Not Connected";
        }
    }

}

