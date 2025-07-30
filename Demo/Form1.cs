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

            AgrineUI.Dialogs.AGColorDialog aGColorDialog = new AgrineUI.Dialogs.AGColorDialog();
            aGColorDialog.ShowDialog();
           
        }

        private void agButton1_Click(object sender, EventArgs e)
        {
            AgrineUI.Forms.AGForm aG = new AgrineUI.Forms.AGForm();
            aG.Show();
        }
    }
}
