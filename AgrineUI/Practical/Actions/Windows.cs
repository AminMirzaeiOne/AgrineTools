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
            None, Shutdown, Restart, Sleep, Look
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
                }
            }
        }

        /// <summary>
        /// Windows operating system shutdown action
        /// </summary>
        /// <param name="delay">Windows shutdown delay value</param>
        public static void Shutdown(byte delay = 5)
        {
            Process.Start($"shutdown", "/s /t " + delay);
        }

        /// <summary>
        /// Windows operating system restart action
        /// </summary>
        /// <param name="delay">Windows restart delay value</param>
        public static void Restart(byte delay = 5)
        {
            Process.Start($"shutdown", "/r /t " + delay);
        }

    }
}
