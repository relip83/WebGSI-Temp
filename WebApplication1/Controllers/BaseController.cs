using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers
{
    abstract public class BaseController :ApiController
    {
        public BL.ViewModelLayer.Setting.ViewModelLayerSettings CurrentSetting { get; internal set; }

        protected T CreateGSIManager<T>() where T : Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base.BaseViewModelManager
        {
            var manager = Activator.CreateInstance(typeof(T), CurrentSetting) as Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base.BaseViewModelManager;

            return (T)manager;
        }
    }
}