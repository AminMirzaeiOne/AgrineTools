using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Interfaces
{
    public interface IAGShape
    {
        bool AgreeTheme { get; set; }
        byte BorderSize { get; set; }
        System.Drawing.Color BorderColor { get; set; }
        
    }
}
