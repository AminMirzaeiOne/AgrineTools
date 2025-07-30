using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : AgrineUI.Forms.AGForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonX1_Click(object sender, EventArgs e)
        {
            bool x = await AgrineUI.Practical.Actions.Windows.CheckPing();
            MessageBox.Show(x.ToString());

           
        }

    }
}
