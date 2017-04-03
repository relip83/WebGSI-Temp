using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class DeviceTypes
    {
        public static DeviceTypes DeviceTypesSetting { set; get; }
        public  BaseType[] WaterPulseTypes { set; get; }

        public  BaseType[] FertPulseTypes { set; get; }

        public  BaseType[] LandSizeTypes { set; get; }

        public  BaseType[] CommIntervalTypes { set; get; }

        static DeviceTypes()
        {
            DeviceTypesSetting = new DeviceTypes();
            DeviceTypesSetting.WaterPulseTypes = BaseType.GetValues<WaterMeter_PulseType>().ToArray();
            DeviceTypesSetting.CommIntervalTypes = BaseType.GetValues<CommIntervalType>().ToArray();
            DeviceTypesSetting.FertPulseTypes = BaseType.GetValues<Fertilizer_PulseType>().ToArray();
            DeviceTypesSetting.LandSizeTypes = BaseType.GetValues<LandType>().ToArray();
        }

        public DeviceTypes()
        {
                
        }
    }
}
