using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AgrineCore.Practical
{
    public static class ImageConverter
    {
        /// <summary>
        /// Converts image file from one format to another.
        /// Supported formats: jpg, png, bmp, gif, tiff
        /// </summary>
        public static bool ConvertImageFormat(string inputPath, string outputPath, ImageFormat outputFormat)
        {
            try
            {
                if (!File.Exists(inputPath))
                    return false;

                using (var image = Image.FromFile(inputPath))
                {
                    image.Save(outputPath, outputFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Helper to get ImageFormat by extension
        /// </summary>
        public static ImageFormat GetImageFormat(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;

            extension = extension.ToLower().TrimStart('.');

            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "bmp":
                    return ImageFormat.Bmp;
                case "gif":
                    return ImageFormat.Gif;
                case "tiff":
                case "tif":
                    return ImageFormat.Tiff;
                default:
                    return null;
            }
        }
    }
}
