using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting
{
    public  class AdvancedSettingsView
    {
        #region ctor

        public AdvancedSettingsView()
        {
                
        }
        public AdvancedSettingsView(GeneralSettings advancedSettings)
        {
            if (advancedSettings == null)
                return;
            IsMetric = advancedSettings.IsMetric;
            LandType = (LandType)advancedSettings.LandTypeID;
        }
       
        #endregion

        public bool IsMetric { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LandType LandType { get; set; }
        public int ZoneOverlapping { get; set; }
    }
}
