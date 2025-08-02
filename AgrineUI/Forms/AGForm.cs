using AgrineUI.Controls;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            this.PaletteCheck();
            this.BackColorCheck();
            this.ForeColorCheck();
            this.AutoScaleMode = AutoScaleMode.Inherit;
        }

        public AGForm(AGForm parentform)
        {
            InitializeComponent();
            this.ThemeCheck();
            this.PaletteCheck();
            this.BackColorCheck();
            this.ForeColorCheck();
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.DarkMode = parentform.DarkMode;
            this.Palette = parentform.Palette;
        }

        private bool darkMode = false;
        private System.Drawing.Color palette = Color.Tomato;

        [Category("Theme")]
        public bool DarkMode
        {
            get { return this.darkMode; }
            set
            {
                this.darkMode = value;
                this.ThemeCheck();
                this.PaletteCheck();
                this.BackColorCheck();
                this.ForeColorCheck();
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
                this.PaletteCheck();
                this.BackColorCheck();
                this.ForeColorCheck();
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

        private void PaletteCheck()
        {
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGButton>().ToList().ForEach(agbutton => agbutton.BorderColor = this.Palette);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Shapes.AGRectangle>().ToList().ForEach(agrectangle => agrectangle.BorderColor = this.Palette);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Shapes.AGCircle>().ToList().ForEach(agcircle => agcircle.BorderColor = this.Palette);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGCheckBox>().ToList().ForEach(agcheckbox => agcheckbox.Palette = this.Palette);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGRadioButton>().ToList().ForEach(agradiobutton => agradiobutton.Palette = this.Palette);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGTextBox>().ToList().ForEach(agtextbox => agtextbox.BorderColor = this.Palette);
        }

        private void BackColorCheck()
        {
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGCheckBox>().ToList().ForEach(agcheckbox => agcheckbox.BackColor = this.BackColor);
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGRadioButton>().ToList().ForEach(agradiobutton => agradiobutton.BackColor = this.BackColor);
            if (this.DarkMode)
                GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGTextBox>().ToList().ForEach(agtextbox => agtextbox.BackColor = Color.FromArgb(30, 30, 30));
            else
                GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGTextBox>().ToList().ForEach(agtextbox => agtextbox.BackColor = Color.White);

        }

        private void ForeColorCheck()
        {
            Color foreColorControls = IsColorDark(this.BackColor) ? Color.White : Color.Black;
            GetSelfAndChildrenRecursive(this).OfType<AgrineUI.Controls.AGTextBox>().ToList().ForEach(agtextbox => agtextbox.ForeColor = foreColorControls);
        }

        private bool IsColorDark(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }

        public System.Collections.Generic.IEnumerable<System.Windows.Forms.Control> GetSelfAndChildrenRecursive(System.Windows.Forms.Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            }

            controls.Add(parent);

            return controls;
        }

    }
}
