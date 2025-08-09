using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace AgrineCore.Deploy
{
    public static class InstallerManager
    {
        /// <summary>
        /// Runs an installer file (exe, msi, etc.) with optional arguments.
        /// </summary>
        public static void RunInstaller(string installerPath, string arguments = null)
        {
            if (!File.Exists(installerPath))
                throw new FileNotFoundException("Installer file not found.", installerPath);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = installerPath,
                UseShellExecute = true,
                Arguments = arguments ?? string.Empty
            };

            Process.Start(psi);
        }

        /// <summary>
        /// Extracts a ZIP archive to the target folder.
        /// </summary>
        public static void ExtractZip(string zipFilePath, string extractToDirectory)
        {
            if (!File.Exists(zipFilePath))
                throw new FileNotFoundException("ZIP archive not found.", zipFilePath);

            if (!Directory.Exists(extractToDirectory))
                Directory.CreateDirectory(extractToDirectory);

            ZipFile.ExtractToDirectory(zipFilePath, extractToDirectory);
        }

        /// <summary>
        /// Checks the file extension and runs the installer or extracts archive accordingly.
        /// Supports .exe, .msi, and .zip files.
        /// </summary>
        public static void ProcessInstallerFile(string filePath, string extractFolder = null, string installerArguments = null)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Installer file not found.", filePath);

            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            switch (ext)
            {
                case ".exe":
                case ".msi":
                    RunInstaller(filePath, installerArguments);
                    break;

                case ".zip":
                    if (string.IsNullOrEmpty(extractFolder))
                        throw new ArgumentException("Extract folder must be specified for ZIP archives.");

                    ExtractZip(filePath, extractFolder);
                    break;

                default:
                    throw new NotSupportedException("File type not supported for installation: " + ext);
            }
        }
    }
}
