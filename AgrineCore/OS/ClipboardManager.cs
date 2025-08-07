using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AgrineCore.OS
{


    public static class ClipboardManager
    {
        // -------- TEXT --------
        public static void SetText(string text)
        {
            try { Clipboard.SetText(text); }
            catch (Exception ex) { Console.WriteLine("Clipboard text set error: " + ex.Message); }
        }

        public static string GetText()
        {
            try { return Clipboard.ContainsText() ? Clipboard.GetText() : string.Empty; }
            catch { return string.Empty; }
        }

        public static bool HasText()
        {
            try { return Clipboard.ContainsText(); }
            catch { return false; }
        }

        // -------- IMAGE --------
        public static void SetImage(System.Drawing.Image image)
        {
            try { Clipboard.SetImage(image); }
            catch (Exception ex) { Console.WriteLine("Clipboard image set error: " + ex.Message); }
        }

        public static System.Drawing.Image GetImage()
        {
            try { return Clipboard.ContainsImage() ? Clipboard.GetImage() : null; }
            catch { return null; }
        }

        public static bool HasImage()
        {
            try { return Clipboard.ContainsImage(); }
            catch { return false; }
        }

        // -------- FILES --------
        public static void SetFiles(string[] filePaths)
        {
            try
            {
                var fileCollection = new System.Collections.Specialized.StringCollection();
                fileCollection.AddRange(filePaths);
                Clipboard.SetFileDropList(fileCollection);
            }
            catch (Exception ex) { Console.WriteLine("Clipboard file set error: " + ex.Message); }
        }

        public static string[] GetFiles()
        {
            try
            {
                if (Clipboard.ContainsFileDropList())
                {
                    var files = Clipboard.GetFileDropList();
                    var result = new string[files.Count];
                    files.CopyTo(result, 0);
                    return result;
                }
            }
            catch { }
            return Array.Empty<string>();
        }

        public static bool HasFiles()
        {
            try { return Clipboard.ContainsFileDropList(); }
            catch { return false; }
        }

        // -------- CLEAR --------
        public static void Clear()
        {
            try { Clipboard.Clear(); }
            catch (Exception ex) { Console.WriteLine("Clipboard clear error: " + ex.Message); }
        }

        // -------- SYSTEM SUPPORT --------
        public static bool IsClipboardUIAvailable()
        {
            // Windows 10 version 1809 (build 17763) introduced clipboard UI
            try
            {
                Version winVer = Environment.OSVersion.Version;
                return (winVer.Major >= 10 && winVer.Build >= 17763);
            }
            catch { return false; }
        }

        public static void OpenClipboardUI()
        {
            if (IsClipboardUIAvailable())
            {
                try
                {
                    Process.Start("explorer.exe", "ms-settings:clipboard");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to open Clipboard UI: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Clipboard UI not supported on this version of Windows.");
            }
        }

        // -------- DEBUG / MONITOR (Simplified Console) --------
        public static void PrintClipboardInfo()
        {
            Console.WriteLine("Clipboard Info:");
            Console.WriteLine($" - Text: {(HasText() ? GetText() : "No text")}");
            Console.WriteLine($" - Image: {(HasImage() ? "Yes" : "No")}");
            Console.WriteLine($" - Files: {(HasFiles() ? string.Join(", ", GetFiles()) : "No files")}");
        }
    }

}
