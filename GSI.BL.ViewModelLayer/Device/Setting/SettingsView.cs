using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
   
    public  class SettingsView
    {

        public DeviceTypes Types { set; get; }
        public DeviceInfoView DeviceInfo { set; get; }
        public GeneralSettingView GeneralSetting { set; get; }

        public IrrgationSettingView IrrgationSetting { set; get; }

        public WaterMeterSettingView WaterMeter { set; get; }

        public RainSensorView RainSensor { set; get; }

        public FertilizerSettingView Fertilizer { set; get; }

        public AdvancedSettingsView AdvancedSettings { get; set; }

        public SettingsView()
        {
            Types = DeviceTypes.DeviceTypesSetting;
        }
    }
}



