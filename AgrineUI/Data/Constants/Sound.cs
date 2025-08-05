using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Data.Constants
{
    public static class Sound
    {
        public static class Buttons
        {
            public static Stream ASoundClick = Assembly.GetExecutingAssembly().GetManifestResourceStream("AgrineUI.Sounds.AGButton.Click_Sound1.wav");
        }
    }
}
