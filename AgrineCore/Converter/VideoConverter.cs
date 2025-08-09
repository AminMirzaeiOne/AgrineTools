using System;
using System.IO;

namespace AgrineCore.Converter
{
    public static class VideoConverter
    {
        /// <summary>
        /// Note: Real video format conversion requires external libs.
        /// This method just copies the file and changes the extension.
        /// </summary>
        public static bool ChangeVideoExtension(string inputPath, string outputPath)
        {
            try
            {
                if (!File.Exists(inputPath))
                    return false;

                var validExtensions = new[] { ".mp4", ".avi", ".mkv", ".mov", ".wmv" };
                string inputExt = Path.GetExtension(inputPath).ToLower();
                string outputExt = Path.GetExtension(outputPath).ToLower();

                if (Array.IndexOf(validExtensions, inputExt) < 0 ||
                    Array.IndexOf(validExtensions, outputExt) < 0)
                    return false;

                File.Copy(inputPath, outputPath, overwrite: true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
