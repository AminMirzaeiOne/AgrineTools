using System;
using System.Diagnostics;
using System.Reflection;

namespace AgrineCore.Deploy
{
    public static class VersionInfo
    {
        /// <summary>
        /// Gets the FileVersionInfo for a given file path, or null if not available.
        /// </summary>
        public static FileVersionInfo GetVersionInfo(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!System.IO.File.Exists(filePath))
                return null;

            try
            {
                return FileVersionInfo.GetVersionInfo(filePath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the file version string of the given file.
        /// </summary>
        public static string GetFileVersion(string filePath)
        {
            var info = GetVersionInfo(filePath);
            return info?.FileVersion ?? string.Empty;
        }

        /// <summary>
        /// Gets the product version string of the given file.
        /// </summary>
        public static string GetProductVersion(string filePath)
        {
            var info = GetVersionInfo(filePath);
            return info?.ProductVersion ?? string.Empty;
        }

        /// <summary>
        /// Gets the product name string of the given file.
        /// </summary>
        public static string GetProductName(string filePath)
        {
            var info = GetVersionInfo(filePath);
            return info?.ProductName ?? string.Empty;
        }

        /// <summary>
        /// Gets the company name string of the given file.
        /// </summary>
        public static string GetCompanyName(string filePath)
        {
            var info = GetVersionInfo(filePath);
            return info?.CompanyName ?? string.Empty;
        }

        /// <summary>
        /// Gets the file path of the currently running executable automatically.
        /// For Windows Forms apps, this will get the main exe path.
        /// </summary>
        public static string GetCurrentExecutablePath()
        {
            return Assembly.GetEntryAssembly()?.Location;
        }

        /// <summary>
        /// Gets the version of the currently running executable as Version type.
        /// Returns null if not available.
        /// </summary>
        public static Version GetCurrentVersion()
        {
            var path = GetCurrentExecutablePath();
            if (string.IsNullOrEmpty(path))
                return null;

            var versionStr = GetFileVersion(path);
            if (string.IsNullOrEmpty(versionStr))
                return null;

            Version ver;
            if (Version.TryParse(versionStr, out ver))
                return ver;

            return null;
        }

        /// <summary>
        /// Compares two version strings.
        /// Returns:
        /// -1 if version1 < version2
        /// 0 if equal
        /// 1 if version1 > version2
        /// </summary>
        public static int CompareVersions(string version1, string version2)
        {
            if (string.IsNullOrEmpty(version1) && string.IsNullOrEmpty(version2))
                return 0;
            if (string.IsNullOrEmpty(version1))
                return -1;
            if (string.IsNullOrEmpty(version2))
                return 1;

            Version v1, v2;
            if (!Version.TryParse(version1, out v1))
                return -1; // invalid version considered older
            if (!Version.TryParse(version2, out v2))
                return 1;

            return v1.CompareTo(v2);
        }

        /// <summary>
        /// Checks if the current running version is older than the given version string.
        /// Returns true if current version &lt; newVersion (update needed).
        /// </summary>
        public static bool IsUpdateAvailable(string newVersion)
        {
            var currentVersion = GetCurrentVersion();
            if (currentVersion == null)
                return false;

            int cmp = CompareVersions(currentVersion.ToString(), newVersion);
            return cmp < 0;
        }

        /// <summary>
        /// Example: Compares current running version with version info from a file path.
        /// Returns true if an update is available (newer version exists).
        /// </summary>
        public static bool IsUpdateAvailableFromFile(string newVersionFilePath)
        {
            var newVersion = GetFileVersion(newVersionFilePath);
            if (string.IsNullOrEmpty(newVersion))
                return false;

            return IsUpdateAvailable(newVersion);
        }
    }
}
