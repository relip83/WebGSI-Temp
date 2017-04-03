using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device
{
    public class MainPipeSettings
    {
        public bool UseMaster { set; get; }
        public byte WaterFactor { set; get; }
        public bool RainDetectorEnabled { get; set; }
        public byte RainyDaysStopper { get; set; }
        public bool RainDetectorNC { get; set; }

        public bool IsLocalSequenceActive { get; set; }
        public bool ProgramsAsQueue { get; set; }

        public bool DSundayState { set; get; }

        public bool DMondayState { set; get; }

        public bool DTuesdayState { set; get; }

        public bool DWednesdayState { set; get; }

        public bool DThursdayState { set; get; }

        public bool DFridayState { set; get; }

        public bool DSaturdayState { set; get; }
        public int OverlapTime { get; set; }
    }
}
