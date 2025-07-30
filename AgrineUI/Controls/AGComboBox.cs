using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Controls
{
    public class AGComboBox : DevComponents.DotNetBar.Controls.ComboBoxEx
    {
        [Category("Border")]
        public byte BorderSize { get; set; } = 2;

        [Category("Border")]
        public byte BorderRadius { get; set; } = 20;

        [Category("Border")]
        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Tomato;


    }
}
