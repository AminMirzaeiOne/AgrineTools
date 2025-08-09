using System;
using System.IO;

namespace AgrineCore.Practical
{
    public static class Logger
    {
        private static string logFilePath = "app.log";

        public static void SetLogFile(string path)
        {
            logFilePath = path;
        }

        private static void Write(string level, string message)
        {
            try
            {
                var logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                File.AppendAllText(logFilePath, logLine + Environment.NewLine);
            }
            catch { /* ignore logging failures */ }
        }

        public static void Info(string message) => Write("INFO", message);
        public static void Warn(string message) => Write("WARN", message);
        public static void Error(string message) => Write("ERROR", message);
    }
}
