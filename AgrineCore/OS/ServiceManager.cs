using AgrineCore.OS; // برای استفاده از کلاس Cmd
using AgrineCore.Practical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace AgrineCore.OS
{
    public static class ServiceManager
    {
        // Get all services
        public static ServiceController[] GetAllServices()
        {
            return ServiceController.GetServices();
        }

        // Get specific service
        public static ServiceController GetService(string serviceName)
        {
            return GetAllServices().FirstOrDefault(s => s.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
        }

        // Check if a service exists
        public static bool Exists(string serviceName)
        {
            return GetService(serviceName) != null;
        }

        // Get current status of service
        public static ServiceControllerStatus? GetStatus(string serviceName)
        {
            var svc = GetService(serviceName);
            return svc?.Status;
        }

        // Start service (with fallback to PowerShell)
        public static bool Start(string serviceName, int timeoutMs = 10000)
        {
            try
            {
                var svc = GetService(serviceName);
                if (svc != null && svc.Status != ServiceControllerStatus.Running)
                {
                    svc.Start();
                    svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(timeoutMs));
                    return true;
                }
            }
            catch
            {
                // Fallback to PowerShell
                var result = Cmd.RunPowerShell($"Start-Service -Name \"{serviceName}\"");
                return result.Output.ToLower().Contains("success") || result.ExitCode == 0;
            }

            return false;
        }

        // Stop service (with fallback to PowerShell)
        public static bool Stop(string serviceName, int timeoutMs = 10000)
        {
            try
            {
                var svc = GetService(serviceName);
                if (svc != null && svc.Status != ServiceControllerStatus.Stopped)
                {
                    svc.Stop();
                    svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(timeoutMs));
                    return true;
                }
            }
            catch
            {
                // Fallback to PowerShell
                var result = Cmd.RunPowerShell($"Stop-Service -Name \"{serviceName}\" -Force");
                return result.Output.ToLower().Contains("success") || result.ExitCode == 0;
            }

            return false;
        }

        // Restart service (stop then start)
        public static bool Restart(string serviceName, int timeoutMs = 20000)
        {
            return Stop(serviceName, timeoutMs / 2) && Start(serviceName, timeoutMs / 2);
        }

        // Install a Windows Service (requires admin privileges)
        public static bool Install(string exePath)
        {
            // PowerShell command to install service
            var result = Cmd.RunPowerShell($"New-Service -Name \"{System.IO.Path.GetFileNameWithoutExtension(exePath)}\" -Binary \"{exePath}\" -DisplayName \"{System.IO.Path.GetFileNameWithoutExtension(exePath)}\" -StartupType Automatic");
            return result.ExitCode == 0;
        }

        // Delete (uninstall) a Windows service
        public static bool Delete(string serviceName)
        {
            var result = Cmd.Run($"sc delete \"{serviceName}\"");
            return result.Output.ToLower().Contains("success") || result.ExitCode == 0;
        }

        // Get all running services
        public static List<ServiceController> GetRunningServices()
        {
            return GetAllServices().Where(s => s.Status == ServiceControllerStatus.Running).ToList();
        }

        // Get all stopped services
        public static List<ServiceController> GetStoppedServices()
        {
            return GetAllServices().Where(s => s.Status == ServiceControllerStatus.Stopped).ToList();
        }

        // Get services by specific status
        public static List<ServiceController> GetServicesByStatus(ServiceControllerStatus status)
        {
            return GetAllServices().Where(s => s.Status == status).ToList();
        }
    }
}
