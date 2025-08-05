using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Interfaces
{
    public interface IAGControlAnimation
    {
        bool Animation { get; set; }

        AgrineUI.Data.Enums.AnimationControls.AnimationSpeedOptions AnimationSpeed { get; set; }
    }
}
