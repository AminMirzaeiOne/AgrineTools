using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Interfaces
{
    public interface IAGControlTheme
    {
        Color Palette { get; set; }
        bool DarkMode { get; set; }
    }
}
