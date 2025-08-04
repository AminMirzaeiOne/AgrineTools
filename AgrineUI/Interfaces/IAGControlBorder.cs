using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Interfaces
{
    public interface IAGControlBorder
    {
        byte BorderSize { get; set; }

        byte BorderRadius { get; set; }
    }
}
