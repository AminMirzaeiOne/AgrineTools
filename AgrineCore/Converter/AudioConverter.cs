using System;
using System.IO;

namespace AgrineCore.Converter
{
    public static class AudioConverter
    {
        /// <summary>
        /// Note: Real audio format conversion requires external libs.
        /// This method does a simple file copy with changed extension if valid.
        /// </summary>
        public static bool ChangeAudioExtension(string inputPath, string outputPath)
        {
            try
            {
                if (!File.Exists(inputPath))
                    return false;

                var validExtensions = new[] { ".mp3", ".wav", ".aac", ".flac", ".ogg" };
                string inputExt = Path.GetExtension(inputPath).ToLower();
                string outputExt = Path.GetExtension(outputPath).ToLower();

                if (Array.IndexOf(validExtensions, inputExt) < 0 ||
                    Array.IndexOf(validExtensions, outputExt) < 0)
                    return false;

                // Just copy file; this does NOT convert audio data format
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
