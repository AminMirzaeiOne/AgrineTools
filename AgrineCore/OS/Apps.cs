using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace AgrineCore.OS
{
    public static class App
    {
        #region Basic Info & List

        public static List<ProcessInfo> GetAllProcesses()
        {
            var list = new List<ProcessInfo>();
            try
            {
                foreach (var p in Process.GetProcesses())
                {
                    list.Add(GetProcessInfo(p));
                }
            }
            catch { }
            return list.OrderBy(p => p.ProcessName).ToList();
        }

        public static ProcessInfo GetProcessById(int id)
        {
            try
            {
                Process p = Process.GetProcessById(id);
                return GetProcessInfo(p);
            }
            catch { return null; }
        }

        private static ProcessInfo GetProcessInfo(Process p)
        {
            return new ProcessInfo
            {
                Id = p.Id,
                ProcessName = p.ProcessName,
                StartTime = SafeGetStartTime(p),
                MemoryUsageMB = p.WorkingSet64 / (1024.0 * 1024.0),
                ThreadsCount = p.Threads.Count,
                Responding = p.Responding,
                Path = SafeGetMainModuleFileName(p),
                CommandLine = GetCommandLine(p.Id)
            };
        }

        private static DateTime? SafeGetStartTime(Process p)
        {
            try { return p.StartTime; }
            catch { return null; }
        }

        private static string SafeGetMainModuleFileName(Process p)
        {
            try { return p.MainModule.FileName; }
            catch { return "Access Denied or Unknown"; }
        }

        #endregion

        #region Kill / Close / Suspend / Resume

        public static bool KillProcess(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                p.Kill();
                p.WaitForExit(5000);
                return p.HasExited;
            }
            catch { return false; }
        }

        public static int KillProcessesByName(string processName)
        {
            int count = 0;
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process p in processes)
                {
                    try
                    {
                        p.Kill();
                        p.WaitForExit(5000);
                        if (p.HasExited) count++;
                    }
                    catch { }
                }
            }
            catch { }
            return count;
        }

        public static bool CloseProcess(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                if (p.CloseMainWindow())
                {
                    p.WaitForExit(5000);
                    return p.HasExited;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool SuspendProcess(int processId)
        {
            try
            {
                List<IntPtr> threads = GetProcessThreads(processId);
                foreach (IntPtr tid in threads)
                    NtSuspendThread(tid);
                return true;
            }
            catch { return false; }
        }

        public static bool ResumeProcess(int processId)
        {
            try
            {
                List<IntPtr> threads = GetProcessThreads(processId);
                foreach (IntPtr tid in threads)
                    NtResumeThread(tid);
                return true;
            }
            catch { return false; }
        }

        #endregion

        #region Process Threads

        public static List<IntPtr> GetProcessThreads(int processId)
        {
            var threadHandles = new List<IntPtr>();
            try
            {
                Process p = Process.GetProcessById(processId);
                foreach (ProcessThread t in p.Threads)
                {
                    IntPtr threadHandle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, t.Id);
                    if (threadHandle != IntPtr.Zero)
                        threadHandles.Add(threadHandle);
                }
            }
            catch { }
            return threadHandles;
        }

        #endregion

        #region Process Priority & Affinity

        public static bool SetProcessPriority(int processId, ProcessPriorityClass priority)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                p.PriorityClass = priority;
                return true;
            }
            catch { return false; }
        }

        public static ProcessPriorityClass? GetProcessPriority(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                return p.PriorityClass;
            }
            catch { return null; }
        }

        public static bool SetProcessorAffinity(int processId, IntPtr affinityMask)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                p.ProcessorAffinity = affinityMask;
                return true;
            }
            catch { return false; }
        }

        public static IntPtr? GetProcessorAffinity(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                return p.ProcessorAffinity;
            }
            catch { return null; }
        }

        #endregion

        #region CPU Usage & Running Status

        public static double GetProcessCpuUsage(int processId, int durationMs = 500)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                DateTime startTime = DateTime.UtcNow;
                TimeSpan startCpuTime = process.TotalProcessorTime;

                Thread.Sleep(durationMs);

                DateTime endTime = DateTime.UtcNow;
                TimeSpan endCpuTime = process.TotalProcessorTime;

                double cpuUsedMs = (endCpuTime - startCpuTime).TotalMilliseconds;
                double totalMsPassed = (endTime - startTime).TotalMilliseconds;

                int cpuCores = Environment.ProcessorCount;

                double cpuUsageTotal = (cpuUsedMs / (totalMsPassed * cpuCores)) * 100;
                return Math.Round(cpuUsageTotal, 2);
            }
            catch
            {
                return -1;
            }
        }

        public static bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        public static bool IsProcessResponding(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                return p.Responding;
            }
            catch { return false; }
        }

        #endregion

        #region Run Process & Get Output

        public static bool StartProcess(string filePath, string arguments = null, bool runAsAdmin = false)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
                startInfo.UseShellExecute = true;
                startInfo.Arguments = arguments ?? string.Empty;

                if (runAsAdmin)
                    startInfo.Verb = "runas";

                Process.Start(startInfo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Tuple<bool, string, string> RunProcessGetOutput(string filePath, string arguments = null, int timeoutMs = 5000)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
                startInfo.Arguments = arguments ?? "";
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                        return Tuple.Create(false, "", "Failed to start process");

                    bool exited = process.WaitForExit(timeoutMs);
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    return Tuple.Create(exited, output, error);
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, "", ex.Message);
            }
        }

        #endregion

        #region Get Process Command Line Arguments

        public static string GetCommandLine(int processId)
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + processId))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        var commandLine = obj["CommandLine"];
                        if (commandLine != null)
                            return commandLine.ToString();
                    }
                }
            }
            catch { }
            return "";
        }

        #endregion

        #region Native Methods for Suspend/Resume Threads

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern uint NtSuspendThread(IntPtr threadHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern uint NtResumeThread(IntPtr threadHandle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [Flags]
        private enum ThreadAccess
        {
            TERMINATE = 0x0001,
            SUSPEND_RESUME = 0x0002,
            GET_CONTEXT = 0x0008,
            SET_CONTEXT = 0x0010,
            SET_INFORMATION = 0x0020,
            QUERY_INFORMATION = 0x0040,
            SET_THREAD_TOKEN = 0x0080,
            IMPERSONATE = 0x0100,
            DIRECT_IMPERSONATION = 0x0200
        }

        #endregion
    }

    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public DateTime? StartTime { get; set; }
        public double MemoryUsageMB { get; set; }
        public int ThreadsCount { get; set; }
        public bool Responding { get; set; }
        public string Path { get; set; }
        public string CommandLine { get; set; }

        public ProcessInfo()
        {
            ProcessName = "";
            Path = "";
            CommandLine = "";
        }
    }
}