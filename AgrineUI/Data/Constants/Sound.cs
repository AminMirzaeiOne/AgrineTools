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
        public class Buttons
        {
            public readonly Stream ASoundClick = Assembly.GetExecutingAssembly().GetManifestResourceStream("AgrineUI.Sounds.AGButton.ASound_Click.wav");
        }
    }
}
