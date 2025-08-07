using System;
using System.IO;

namespace AgrineCore.OS
{
    using System;
    using System.IO;

    public static class FileManager
    {
        #region Basic File Operations

        public static bool FileExists(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch { return false; }
        }

        public static bool CreateFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                    return true;
                }
                return false; // File already exists
            }
            catch { return false; }
        }

        public static bool DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool CopyFile(string sourcePath, string destinationPath, bool overwrite = false)
        {
            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath, overwrite);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool MoveFile(string sourcePath, string destinationPath)
        {
            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        #endregion

        #region File Content Operations

        public static string ReadAllText(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    return File.ReadAllText(filePath);
                return null;
            }
            catch { return null; }
        }

        public static bool WriteAllText(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                return true;
            }
            catch { return false; }
        }

        #endregion

        #region File Info Properties

        public static long GetFileSize(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    return new FileInfo(filePath).Length;
                return -1;
            }
            catch { return -1; }
        }

        public static DateTime? GetFileCreationTime(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    return File.GetCreationTime(filePath);
                return null;
            }
            catch { return null; }
        }

        public static DateTime? GetFileLastAccessTime(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    return File.GetLastAccessTime(filePath);
                return null;
            }
            catch { return null; }
        }

        public static DateTime? GetFileLastWriteTime(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    return File.GetLastWriteTime(filePath);
                return null;
            }
            catch { return null; }
        }

        #endregion

        #region Path Helpers

        public static string GetFileName(string filePath)
        {
            try { return Path.GetFileName(filePath); }
            catch { return null; }
        }

        public static string GetFileExtension(string filePath)
        {
            try { return Path.GetExtension(filePath); }
            catch { return null; }
        }

        public static string GetDirectoryName(string filePath)
        {
            try { return Path.GetDirectoryName(filePath); }
            catch { return null; }
        }

        public static string CombinePath(params string[] paths)
        {
            try { return Path.Combine(paths); }
            catch { return null; }
        }

        #endregion
    }


}

