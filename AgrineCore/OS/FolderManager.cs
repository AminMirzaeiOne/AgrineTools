using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AgrineCore.OS
{

    public static class FolderManager
    {
        #region Basic Directory Operations

        public static bool DirectoryExists(string directoryPath)
        {
            try
            {
                return Directory.Exists(directoryPath);
            }
            catch { return false; }
        }

        public static bool CreateDirectory(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    return true;
                }
                return false; // Already exists
            }
            catch { return false; }
        }

        public static bool DeleteDirectory(string directoryPath, bool recursive = false)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, recursive);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        #endregion

        #region Directory Content

        public static string[] GetFiles(string directoryPath, string searchPattern = "*.*", bool searchAllDirectories = false)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    return Directory.GetFiles(directoryPath, searchPattern,
                        searchAllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                }
                return new string[0];
            }
            catch { return new string[0]; }
        }

        public static string[] GetDirectories(string directoryPath, bool searchAllDirectories = false)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    return Directory.GetDirectories(directoryPath, "*",
                        searchAllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                }
                return new string[0];
            }
            catch { return new string[0]; }
        }

        #endregion

        #region Extended Features

        // Calculate total size of folder recursively (bytes)
        public static long GetDirectorySize(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                    return -1;

                long size = 0;
                string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        size += fi.Length;
                    }
                    catch { }
                }
                return size;
            }
            catch { return -1; }
        }

        // Copy directory recursively
        public static bool CopyDirectory(string sourceDir, string destinationDir, bool overwrite = false)
        {
            try
            {
                if (!Directory.Exists(sourceDir))
                    return false;

                if (!Directory.Exists(destinationDir))
                    Directory.CreateDirectory(destinationDir);

                foreach (string file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                    File.Copy(file, destFile, overwrite);
                }

                foreach (string directory in Directory.GetDirectories(sourceDir))
                {
                    string destSubDir = Path.Combine(destinationDir, Path.GetFileName(directory));
                    CopyDirectory(directory, destSubDir, overwrite);
                }

                return true;
            }
            catch { return false; }
        }

        // Move directory (uses Directory.Move internally)
        public static bool MoveDirectory(string sourceDir, string destinationDir)
        {
            try
            {
                if (!Directory.Exists(sourceDir))
                    return false;

                if (Directory.Exists(destinationDir))
                    return false; // Prevent overwrite

                Directory.Move(sourceDir, destinationDir);
                return true;
            }
            catch { return false; }
        }

        // Clear all contents inside directory but keep directory itself
        public static bool ClearDirectory(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                    return false;

                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    File.Delete(file);
                }

                foreach (string dir in Directory.GetDirectories(directoryPath))
                {
                    Directory.Delete(dir, true);
                }

                return true;
            }
            catch { return false; }
        }

        // Get count of files and directories inside folder (non-recursive)
        public class FolderContentCount
        {
            public int FileCount { get; set; }
            public int DirectoryCount { get; set; }

            public FolderContentCount(int files, int directories)
            {
                FileCount = files;
                DirectoryCount = directories;
            }
        }

        public static FolderContentCount GetFolderContentCount(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                    return new FolderContentCount(0, 0);

                int files = Directory.GetFiles(directoryPath).Length;
                int dirs = Directory.GetDirectories(directoryPath).Length;
                return new FolderContentCount(files, dirs);
            }
            catch
            {
                return new FolderContentCount(0, 0);
            }
        }


        // Check if directory is writable by attempting to create a temp file
        public static bool IsDirectoryWritable(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                    return false;

                string testFile = Path.Combine(directoryPath, Path.GetRandomFileName());
                using (FileStream fs = File.Create(testFile, 1, FileOptions.DeleteOnClose))
                {
                    // If file created successfully, directory is writable
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Path Helpers

        public static string GetDirectoryName(string directoryPath)
        {
            try { return Path.GetFileName(directoryPath.TrimEnd(Path.DirectorySeparatorChar)); }
            catch { return null; }
        }

        public static string GetParentDirectory(string directoryPath)
        {
            try
            {
                DirectoryInfo dirInfo = Directory.GetParent(directoryPath);
                return dirInfo?.FullName;
            }
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
