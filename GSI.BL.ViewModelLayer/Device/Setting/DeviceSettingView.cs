using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Zone;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class DeviceSettingTitleView
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public LandType LandTypeID { get; set; }
        public bool IsAdvancedFert { get; set; }

        public ZoneInfoView[] Zones { get; set; }
        public ValidDaysView ValidDays { get; set; }
        public WaterMeterSettingView WaterMeter { get; set; }
        public FertilizerSettingView Fertilizer { get; set; }


        public DeviceSettingTitleView(FertilizerSetting fert, 
                                      WaterMeterSetting waterMeter ,
                                      GeneralSettings setting , 
                                      MainPipeSettings MainPipe,
                                      ZoneSetting[] zonelist)
        {
            if (setting != null)
            {
                LandTypeID = (LandType)setting.LandTypeID;
                IsAdvancedFert = setting.Flag_AdvancedFert;
            }

            if (zonelist != null)
            {
                Zones = zonelist.Select(z => new ZoneInfoView()
                {
                    Color = z.ZoneColor,
                    Name = z.Name,
                    ZoneNum = z.OutputNumber ,
                    Precipitation_AllowDuration = z.PrecipitationRate !=null && z.PrecipitationRate > 0,
                    Precipitation_AllowQuantity = z.IrrigrationArea != null && z.IrrigrationArea > 0
                }
                ).ToArray();
            }
            Fertilizer = new FertilizerSettingView(fert);
            WaterMeter = new WaterMeterSettingView(waterMeter);
            ValidDays = new ValidDaysView(MainPipe);

        }
    }
}
