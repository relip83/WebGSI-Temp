using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Galcon.GSI.Systems.GSIGroup;

namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers.Setting
{
    public class CarrierSettings// : CommonWebAPI.Settings.SettingsCarrier
    {
        #region properties

        public BL.ViewModelLayer.Setting.ViewModelLayerSettings ViewModelLayerSettings { get; set; }

        #endregion

        #region ctor

        public CarrierSettings()
           // : base()
        {
            ViewModelLayerSettings = new BL.ViewModelLayer.Setting.ViewModelLayerSettings();
        }

        #endregion

        #region override from [CommonWebAPI.Settings.SettingsCarrier]

        protected  void OnRead()
        {
          // this.ViewModelLayerSettings = JsonHelpersLibrary.SettingsLocator.Locator.Read<BL.ViewModelLayer.Settings.ViewModelLayerSettings>(BL.ViewModelLayer.Settings.ViewModelLayerSettings.SETTING_LOCATOR_NAME);
        }

        #endregion
    }
}