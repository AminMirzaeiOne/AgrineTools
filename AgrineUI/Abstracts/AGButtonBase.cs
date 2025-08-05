using AgrineUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Abstracts
{
    [ToolboxItem(false)]
    public class AGButtonBase : System.Windows.Forms.Button, IAGControlTheme
    {
        public Color Palette { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DarkMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
