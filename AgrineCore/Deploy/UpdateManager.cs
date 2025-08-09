using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AgrineCore.Deploy
{
    public class UpdateInfo
    {
        public string Version { get; set; }
        public string DownloadUrl { get; set; }
        public string Changelog { get; set; }
    }

    public static class UpdateManager
    {
        /// <summary>
        /// Gets the current application version.
        /// </summary>
        public static Version GetCurrentVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version;
        }

        /// <summary>
        /// Fetches update info from a JSON file hosted on server or locally.
        /// JSON format example: 
        /// { "Version": "1.2.0", "DownloadUrl": "https://example.com/update.exe", "Changelog": "Some changes..." }
        /// </summary>
        public static UpdateInfo GetUpdateInfo(string updateInfoUrl)
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(updateInfoUrl);

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(UpdateInfo));
                    return (UpdateInfo)serializer.ReadObject(ms);
                }
            }
        }

        /// <summary>
        /// Checks if a newer version is available.
        /// </summary>
        public static bool IsUpdateAvailable(UpdateInfo updateInfo)
        {
            Version currentVersion = GetCurrentVersion();
            Version newVersion = new Version(updateInfo.Version);

            return newVersion > currentVersion;
        }

        /// <summary>
        /// Downloads the update file to a specified path.
        /// </summary>
        public static void DownloadUpdate(UpdateInfo updateInfo, string savePath, Action<int> progressChanged = null)
        {
            using (WebClient client = new WebClient())
            {
                if (progressChanged != null)
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        progressChanged(e.ProgressPercentage);
                    };
                }

                client.DownloadFile(updateInfo.DownloadUrl, savePath);
            }
        }

        /// <summary>
        /// Runs the downloaded installer/update file.
        /// </summary>
        public static void RunInstaller(string installerPath)
        {
            if (!File.Exists(installerPath))
                throw new FileNotFoundException("Installer file not found.", installerPath);

            Process.Start(new ProcessStartInfo
            {
                FileName = installerPath,
                UseShellExecute = true
            });
        }
    }
}
