using Galcon.GSI.Systems.GSIGroup.WebServices.Controllers.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers
{
    public class BaseControllerDependencyResolver : IDependencyResolver
    {
        public BL.ViewModelLayer.Setting.ViewModelLayerSettings CurrentSetting { get; set; }

        public BaseControllerDependencyResolver(BL.ViewModelLayer.Setting.ViewModelLayerSettings currentSettings)
            : base()
        {
            CurrentSetting = currentSettings;
        }
       
        #region override from IDependencyResolver

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(Controllers.BaseController) || serviceType.IsSubclassOf(typeof(BaseController)))
            {
                var controller = Activator.CreateInstance(serviceType) as Controllers.BaseController;
                controller.CurrentSetting = CurrentSetting;

                return controller;
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }

        public void Dispose()
        {
        }

        #endregion
    }
}