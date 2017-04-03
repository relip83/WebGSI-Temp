using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device
{
    public class WaterMeterSetting
    {
        public long WaterMeterID { get; set; }
        public int PulseSize { get; set; }
        public byte MeterTypeID { get; set; }
        public bool IsEnabled { get; set; }
        public byte PulseTypeID { get; set; }
        public byte FlowTypeID { get; set; }
        public int NoWaterPulseDelay { get; set; }
        public int LeakageLimit { get; set; }

    }
}
