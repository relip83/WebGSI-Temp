using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ZoneScheduledItemView
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ZoneScheduledInfoView Zone { get; set; }
        public int Duration { get; set; }
        public int ZoneProportion { get; set; }
        public Decimal FertQuant { get; set; }
        public int FertTime { get; set; }
        public int WaterBefore { get; set; }
        public int WaterAfter { get; set; }
    }
}
