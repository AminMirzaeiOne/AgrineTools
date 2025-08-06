using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineCore.Base
{
    public class StartProgram
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
    }
}
