using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Interfaces
{
    public interface IAGUserControlBorder
    {
        bool BorderRadiusCustom { get; set; }
        byte BorderSize { get; set; }
        byte BorderRadius { get; set; }
        AgrineUI.Abstracts.AGUserControlBase.SpecializedRadius CustomRadius { get; set; }
    }
}
