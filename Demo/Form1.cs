using AgrineUI.Controls.Foundation;
using AgrineUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Demo
{
    public partial class Form1 : AgrineUI.Forms.AGForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void agButton1_Click_1(object sender, EventArgs e)
        {
            var resources = typeof(AGSwitchButton).Assembly.GetManifestResourceNames();
            foreach (var name in resources)
            {
                System.Diagnostics.Debug.WriteLine(name);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
