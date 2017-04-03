using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class WaterMeterSettingView
    {
        public int PulseSize { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterMeterType MeterType { get; set; }
        public bool IsEnabled { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterMeter_PulseType PulseTypeID { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterMeter_FlowType FlowTypeID { get; set; }

        public int NoWaterPulseDelay { get; set; }
        public int LeakageLimit { get; set; }

        public WaterMeterSettingView()
        {

        }

        public WaterMeterSettingView(WaterMeterSetting w)
        {
            if (w == null)
                return;
            MeterType = (WaterMeterType)w.MeterTypeID;
            PulseSize = w.PulseSize;
            IsEnabled = w.IsEnabled;
            PulseTypeID = (WaterMeter_PulseType)w.PulseTypeID;
            FlowTypeID = (WaterMeter_FlowType)w.FlowTypeID;
            NoWaterPulseDelay = w.NoWaterPulseDelay;
            LeakageLimit = w.LeakageLimit;
        }
    }
}


