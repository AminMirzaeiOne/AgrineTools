using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Practical.Actions
{
    public static class Windows
    {
        public enum OperationTypes
        {
            None, Shutdown, Restart, Sleep, Lockup
        }

        private static AgrineUI.Practical.Actions.Windows.OperationTypes operation = OperationTypes.None;

        public static AgrineUI.Practical.Actions.Windows.OperationTypes Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                switch (value)
                {
                    case OperationTypes.Shutdown:
                        AgrineUI.Practical.Actions.Windows.Shutdown();
                        break;

                    case OperationTypes.Restart:
                        AgrineUI.Practical.Actions.Windows.Restart();
                        break;

                    case OperationTypes.Sleep:
                        AgrineUI.Practical.Actions.Windows.Sleep();
                        break;

                    case OperationTypes.Lockup:
                        AgrineUI.Practical.Actions.Windows.Lockup();
                        break;
                }
            }
        }

        /// <summary>
        /// Windows operating system shutdown action
        /// </summary>
        /// <param name="delay">Windows shutdown delay value (Based on sec)</param>
        public static void Shutdown(byte delay = 5)
        {
            Process.Start($"shutdown", "/s /t " + delay);
        }

        /// <summary>
        /// Windows operating system restart action
        /// </summary>
        /// <param name="delay">Windows restart delay value (Based on sec)</param>
        public static void Restart(byte delay = 5)
        {
            Process.Start($"shutdown", "/r /t " + delay);
        }

        /// <summary>
        /// Windows operating system sleep action
        /// </summary>
        /// <param name="delay">Windows sleep delay value (Based on sec)</param>
        public static async Task Sleep(byte delay = 5)
        {
            await Task.Delay(delay * 1000);
            Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
        }

        /// <summary>
        /// Windows operating system Lockup action
        /// </summary>
        /// <param name="delay">Windows lockup delay value (Based on sec)</param>
        public static async Task Lockup(byte delay = 5)
        {
            await Task.Delay(delay * 1000);
            Process.Start("rundll32.exe", "user32.dll,LockWorkStation");
        }



    }
}
