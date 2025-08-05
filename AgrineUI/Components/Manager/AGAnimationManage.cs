using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Components.Manager
{
    public partial class AGAnimationManage : Component
    {
        public AGAnimationManage()
        {
            InitializeComponent();
        }

        public AGAnimationManage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool animation = true;
        private AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions animationSpeedOptions = Data.Enums.AnimationControls.AnimationSpeedOptions.Medium;

        [Category("Aniamtion")]
        public bool Animation
        {
            get { return this.animation; }
            set
            {
                this.animation = value;
            }
        }

        [Category("Animatoin")]
        public AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions AnimationSpeedOptions
        {
            get { return this.animationSpeedOptions;}
            set
            {
                this.animationSpeedOptions = value;
            }
        }
        
    }
}
