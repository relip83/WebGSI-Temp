using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class DeviceAlertSettingsView
    {
        #region properties

        public int AlertCode { get; set; }
        public bool IsEnable { get; set; }
        public bool SendSMS { get; set; }
        public bool SendEmail { get; set; }

        public bool IsVisible { get; set; }

        public string Name { get; set; }


        #endregion

        public DeviceAlertSettingsView()
        {

        }
    }
}
