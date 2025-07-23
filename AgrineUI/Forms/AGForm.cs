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
            this.ThemeCheck();
        }

        private bool darkMode = false;
        private System.Drawing.Color palette = Color.CornflowerBlue;

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set 
            { 
                this.darkMode = value;
                this.ThemeCheck();
            }
        }

        [Category("Theme")]
        public System.Drawing.Color Palette
        {
            get { return this.palette; }
            set
            {
                this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(this.styleManager1.MetroColorParameters.CanvasColor, value);
                this.palette = value;
                this.ThemeCheck();
            }
        }

        private void ThemeCheck()
        {
            if (this.DarkMode)
            {
                this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Dark;
                this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.FromArgb(45, 45, 48), this.palette);
                this.BackColor = Color.FromArgb(25, 25, 25);
            }
            else
            {
                this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Light;
                this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.FromArgb(239, 239, 242), this.palette);
                this.BackColor = Color.White;
            }
        }

    }
}
