using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers
{
    [System.Web.Http.RoutePrefix("api/Device")]
    public class DeviceController : BaseController
    {
        #region Setting

        [HttpGet]
        [Route("{SN}/Settings")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetSettings(String SN)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Device.SettingsView>(() => DeviceManager.GetSettingsView(SN));
        }

        

        [HttpGet]
        [Route("{SN}/Config")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetConfig(String SN)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        [Route("{SN}/Settings")]
        public Response SaveSettings(String SN, BL.ViewModelLayer.Device.SettingsView Setting)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return new Response()
            {
                Result = DeviceManager.SaveSettings(SN, Setting)
            };
        }


        #endregion

        #region ZonesSettings

        [HttpGet]
        [Route("{SN}/ZonesSettings")]
        public Response<BL.ViewModelLayer.Zone.ZoneSettingView[]> GetZonesSettings(String SN)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Zone.ZoneSettingView[]>(() => DeviceManager.GetZoneSetting(SN));
        }


        [HttpPost]
        [Route("{SN}/ZonesSettings")]
        public Response SaveZonesSettings(String SN, BL.ViewModelLayer.Zone.ZoneSettingView[] list)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return new Response()
            {
                Result = DeviceManager.SaveZoneSetting(SN, list)
            };
        }

        #endregion

        #region Alert

        [HttpGet]
        [Route("{SN}/AlertsSettings")]
        public Response<BL.ViewModelLayer.Device.AlertSettingsView[]> GetAlertsSettings(String SN, bool SendDefaults = false)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Device.AlertSettingsView[]>(() => DeviceManager.GetAlertsSettings(SN, SendDefaults));
        }


        [HttpPost]
        [Route("{SN}/AlertsSettings")]
        public Response SaveAlertsSettings(String SN, BL.ViewModelLayer.Device.DeviceAlertSettingsView[] list)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();

            return this.HandleResponse <Response>(()=>
                                                     new Response()
                                                    {
                                                        Result = DeviceManager.SaveAlertsSettings(SN, list)
                                                    });
        }


        #endregion

        #region Program


        [HttpGet]
        [Route("{SN}/ProgramsList")]
        public Response<BL.ViewModelLayer.Program.ProgramTitleView[]> GetProgramList(String SN)
        {
            var DeviceManager = CreateGSIManager<BL.ViewModelLayer.Device.DeviceModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Program.ProgramTitleView[]>(() => DeviceManager.GetProgramList(SN));
        }

        #endregion

        #region logs
        [HttpGet]
        [Route("{SN}/Irrigations")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetIrrigations(String SN)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{SN}/Alerts")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetAlerts(String SN)
        {
            throw new NotImplementedException();
        }



        [HttpGet]
        [Route("{SN}/CachedLogs")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetCachedLogs(String SN)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{SN}/Logs")]
        public Response<BL.ViewModelLayer.Device.SettingsView> GetLogs(String SN)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}