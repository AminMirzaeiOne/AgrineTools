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

        [Category("Theme")]
        public DevComponents.DotNetBar.eStyle Style
        {
            get { return this.styleManager1.ManagerStyle; }
            set { this.styleManager1.ManagerStyle = value; }
        }
    }
}
