using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers
{
    [RoutePrefix("api/Zone")]
    public class GSI_ZoneController : BaseController
    {

        #region Settings
        [HttpGet]
        [Route("{SN}/{ZoneNumber}/Settings")]
        public Response<BL.ViewModelLayer.Zone.ZoneSettingView> GetZoneSetting(string SN, int ZoneNumber)
        {
            var ZoneManager = CreateGSIManager<BL.ViewModelLayer.Zone.ZoneModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Zone.ZoneSettingView>(() => ZoneManager.GetZoneSetting(SN, ZoneNumber));
        }


        [HttpPost]
        [Route("{SN}/{ZoneNumber}/Settings")]
        public Response SaveZoneSetting(string SN, int ZoneNumber, BL.ViewModelLayer.Zone.ZoneSettingView setting)
        {
            var ZoneManager = CreateGSIManager<BL.ViewModelLayer.Zone.ZoneModelManager>();
            return new Response()
            {
                Result = ZoneManager.SaveZoneSetting(SN, ZoneNumber, setting)
            };

        }

        #endregion
    }
}