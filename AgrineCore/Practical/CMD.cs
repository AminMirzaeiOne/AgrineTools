using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AgrineCore.Practical
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;

    public class CmdResult
    {
        public string Output { get; set; }
        public string Error { get; set; }
        public int ExitCode { get; set; }
    }

    public static class Cmd
    {
        /// <summary>
        /// Executes a CMD command and returns output, error, and exit code (synchronously)
        /// </summary>
        public static CmdResult Run(string command, int timeoutMs = 10000, string workingDirectory = null)
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + command;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                if (!string.IsNullOrEmpty(workingDirectory))
                    process.StartInfo.WorkingDirectory = workingDirectory;

                process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                bool exited = process.WaitForExit(timeoutMs);
                if (!exited)
                    process.Kill();

                return new CmdResult
                {
                    Output = outputBuilder.ToString().Trim(),
                    Error = errorBuilder.ToString().Trim(),
                    ExitCode = process.ExitCode
                };
            }
        }

        /// <summary>
        /// Executes a CMD command asynchronously
        /// </summary>
        public static async Task<CmdResult> RunAsync(string command, string workingDirectory = null)
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                if (!string.IsNullOrEmpty(workingDirectory))
                    process.StartInfo.WorkingDirectory = workingDirectory;

                process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

                var tcs = new TaskCompletionSource<bool>();

                process.Exited += (s, e) => tcs.TrySetResult(true);
                process.EnableRaisingEvents = true;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await Task.Run(() => process.WaitForExit());

                return new CmdResult
                {
                    Output = outputBuilder.ToString().Trim(),
                    Error = errorBuilder.ToString().Trim(),
                    ExitCode = process.ExitCode
                };
            }
        }

        /// <summary>
        /// Executes a CMD command silently without capturing output or waiting
        /// </summary>
        public static void RunSilent(string command, bool runAsAdmin = false)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                if (runAsAdmin)
                    psi.Verb = "runas";

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing command:\n" + ex.Message);
            }
        }
    }

}
