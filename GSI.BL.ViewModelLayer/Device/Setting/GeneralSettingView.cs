using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
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
    public class GeneralSettingView
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { set; get; }
        public bool UseMaster { set; get; }
        public int TimeZoneID { set; get; }
        public ValidDaysView ValidDays { get; set; }

        public GeneralSettingView()
        {

        }

        public GeneralSettingView(GeneralSettings gSetting, MainPipeSettings pipe)
        {
            Status = (StatusType)gSetting.Status;
            UseMaster = pipe.UseMaster;
            TimeZoneID = gSetting.CustomTimeZoneID;
            ValidDays = new ValidDaysView(pipe);
        }
    }
}
