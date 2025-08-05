using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Components.Manager
{
    public partial class AGSoundManage : Component
    {
        public AGSoundManage()
        {
            InitializeComponent();
        }

        public AGSoundManage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool sound = true;

        [Category("Sound")]
        public bool Sound
        {
            get { return this.sound; }
            set
            {
                this.sound = value;
            }
        }
    }
}
