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


        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DarkMode = !this.DarkMode;
            this.Palette = Color.Crimson;
        }

        private void agButton1_Click(object sender, EventArgs e)
        {
            AgrineUI.Forms.AGForm aG = new AgrineUI.Forms.AGForm(this);
            aG.Show();
        }
    }
}
