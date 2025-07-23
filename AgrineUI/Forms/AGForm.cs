using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Forms
{
    public partial class AGForm : DevComponents.DotNetBar.Office2007Form
    {
        public AGForm()
        {
            InitializeComponent();
        }

        private bool darkMode = false;

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set 
            { 
                this.darkMode = value;
                if (value)
                {
                    this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Dark;
                    this.BackColor = Color.FromArgb(20, 20, 20);
                }
                else
                {
                    this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Light;
                    this.BackColor = Color.White;
                }
            }
        }

        [Category("Theme")]
        public System.Drawing.Color Palette
        {
            get { return this.styleManager1.MetroColorParameters.BaseColor; }
            set
            {
                this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(this.styleManager1.MetroColorParameters.CanvasColor, value);
            }
        }

    }
}
