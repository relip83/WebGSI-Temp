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
    [System.Web.Http.RoutePrefix("api/Program")]
    public class GSI_ProgramController : BaseController
    {

        #region Setting

        [HttpGet]
        [Route("{SN}/{ProgramID}/Settings")]
        public Response<BL.ViewModelLayer.Program.ProgramSettingMainView> GetSettings(String SN, long ProgramID)
        {
            var ProgramManager = CreateGSIManager<BL.ViewModelLayer.Program.ProgramModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Program.ProgramSettingMainView>(() => ProgramManager.GetSettings(SN, ProgramID));
        }

        [HttpPost]
        [Route("{SN}/{ProgramID}/Settings")]
        public Response SaveSettings(String SN, long ProgramID, BL.ViewModelLayer.Program.ProgramSettingView Setting)
        {
            var ProgramManager = CreateGSIManager<BL.ViewModelLayer.Program.ProgramModelManager>();
            return new Response()
            {
                Result = ProgramManager.SaveSettings(SN,ProgramID,Setting)
            };
        }


        #endregion

        #region Scheduled

        [HttpGet]
        [Route("{SN}/Scheduled")]
        public Response<BL.ViewModelLayer.Program.ScheduledIrrigationView[]> GetScheduled(String SN,long StartTicks, long EndTicks)
        {
            var ProgramManager = CreateGSIManager<BL.ViewModelLayer.Program.ProgramModelManager>();
            return this.HandleResponse<BL.ViewModelLayer.Program.ScheduledIrrigationView[]>(() => ProgramManager.GetScheduled(SN, StartTicks, EndTicks));
        }

        #endregion

    }
}