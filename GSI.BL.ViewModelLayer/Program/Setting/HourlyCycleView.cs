using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class HourlyCycleView
    {
        public byte HourlyCyclesPerDay { get; set; }
        public int HourlyCyclesStartTime { get; set; }
        public int HourlyCycleTime { get; set; } // Value is is represented in seconds ->DB and  minute -> GUI
        public int HourlyCyclesStopTime { get; set; }

    }
}
