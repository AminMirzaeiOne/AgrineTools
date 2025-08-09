using System;
using System.IO;
using System.IO.Compression;

namespace AgrineCore.Practical
{
    public static class ZipManager
    {
        public static bool CreateZip(string sourceFolder, string zipFilePath)
        {
            try
            {
                if (File.Exists(zipFilePath))
                    File.Delete(zipFilePath);

                ZipFile.CreateFromDirectory(sourceFolder, zipFilePath, CompressionLevel.Optimal, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ExtractZip(string zipFilePath, string destinationFolder)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFilePath, destinationFolder);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
