using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone
{
    public class ZoneSetting
    {
        public long ConfigID { get; set; }
        public string Name { get; set; }
        public byte OutputNumber { set; get; }
        public int ZoneColor { set; get; }
        public byte StatusID { get; set; }
        public byte TypeID { get; set; }
        public decimal? SetupNominalFlow { get; set; }
        public decimal LastFlow { get; set; }
        public DateTime LastFlow_Date { get; set; }
        public byte LastFlow_FlowTypeID { get; set; }
        public byte LowFlowDeviation { get; set; }   // Value is represented in % 
        public short LowFlowFaultDelay { get; set; }       // Value is is represented in seconds ->DB and  minute -> GUI
        public byte HighFlowDeviation { get; set; }  // Value is represented in % 
        public short HighFlowFaultDelay { get; set; }      // Value is is represented in seconds ->DB and  minute -> GUI
        public short TimeFillDelay { get; set; }       // Value is is represented in seconds ->DB and  minute -> GUI
        public decimal? IrrigrationArea { get; set; }
        public decimal? PrecipitationRate { get; set; }
        public bool FertilizerConnected { get; set; }
        public bool StopOnFertFailure { get; set; }

    }
}
