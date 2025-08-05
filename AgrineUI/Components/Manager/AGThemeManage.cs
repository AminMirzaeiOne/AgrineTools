using AgrineUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Components.Manager
{
    public partial class AGThemeManage : Component
    {
        public AGThemeManage()
        {
            InitializeComponent();
        }

        public AGThemeManage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool darkMode = false;
        private System.Drawing.Color palette = Color.Tomato;

        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                this.ApplyTheme();
            }
        }

        public System.Drawing.Color Palette
        {
            get { return this.palette; }
            set
            {
                this.palette = value;
                this.ApplyPalette();
            }
        }

        private void ApplyTheme()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is AGForm agForm)
                {
                    agForm.DarkMode = this.darkMode;
                }
            }
        }

        private void ApplyPalette()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is AGForm agForm)
                {
                    agForm.Palette = this.palette;
                }
            }
        }
    }
}
