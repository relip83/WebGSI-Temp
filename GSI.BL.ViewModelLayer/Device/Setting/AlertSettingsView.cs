using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class AlertSettingsView
    {
        public DeviceAlertSettingsView Device_Settings { set; get; }
        public DeviceAlertSettingsView Default_Settings { set; get; }

        public AlertSettingsView()
        {
            Device_Settings = new DeviceAlertSettingsView();
            Default_Settings = new DeviceAlertSettingsView();
        }



        public AlertSettingsView(AlertsSetting AlertsSetting, bool SendDefaults)
        {
            Device_Settings = new DeviceAlertSettingsView()
            {
                AlertCode = AlertsSetting.AlertCode,
                Name = AlertsSetting.Name,
                IsEnable = AlertsSetting.IsEnable.HasValue ? AlertsSetting.IsEnable.Value : AlertsSetting.Default_IsActive,
                SendEmail = AlertsSetting.SendEmail.HasValue ? AlertsSetting.SendEmail.Value : AlertsSetting.Default_SendEmail,
                SendSMS = AlertsSetting.SendSMS.HasValue ? AlertsSetting.SendSMS.Value : AlertsSetting.Default_SendSMS
            };

            if (SendDefaults)
            {
                Default_Settings = new DeviceAlertSettingsView()
                {
                    AlertCode = AlertsSetting.AlertCode,
                    Name = AlertsSetting.Name,
                    IsEnable = AlertsSetting.Default_IsActive,
                    SendEmail = AlertsSetting.Default_SendEmail,
                    SendSMS = AlertsSetting.Default_SendSMS
                };
            }
        }
    }
}
