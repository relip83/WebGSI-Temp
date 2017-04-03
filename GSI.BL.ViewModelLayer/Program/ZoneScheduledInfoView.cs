using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ZoneScheduledInfoView
    {
        public int ZoneNum { get; set; }
        public string Name { get; set; }
        public int Color { get; set; } // Value is in "0x000000-0xFFFFFF" -> (int) 0 - 16777215
    }
}
