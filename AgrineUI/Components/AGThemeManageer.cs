using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Components
{
    public partial class AGThemeManageer : Component
    {
        public AGThemeManageer()
        {
            InitializeComponent();
        }

        public AGThemeManageer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
