using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Galcon.GSI.Systems.GSIGroup.WebServices.Controllers.Setting;
using System.Web.Mvc;
using System.Web.Http;
using Galcon.GSI.Systems.GSIGroup.WebServices.Controllers;

[assembly: OwinStartup(typeof(Galcon.GSI.Systems.GSIGroup.WebServices.Startup))]
namespace Galcon.GSI.Systems.GSIGroup.WebServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            #region read settings

           // JsonHelpersLibrary.SettingsLocator.Locator.DefaultFolderLocation = System.Web.Hosting.HostingEnvironment.MapPath("~");

            var _Settings = new CarrierSettings();
           

            _Settings.ViewModelLayerSettings.AdminRepositoryFunc = () =>
            {
                return new Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.T_SQL.TSQLAdminRepository("GSI-Group_AdminDB");
            };

            //_Settings.ViewModelLayerSettings.AgricultureRepositoryFunc = () =>
            //{

            //    return new Galcon.GSI.Systems.XCIGroup.BL.Bulks.AgricultureData.ES.ESAgricultureRepository(new ElasticSettings());
            //};

            #endregion

            AreaRegistration.RegisterAllAreas();
          //  GlobalConfiguration.Configure(CommonWebApiConfig.Register);
            App_Start.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            #region DependencyResolver

            var _DependencyResolver = new BaseControllerDependencyResolver(_Settings.ViewModelLayerSettings);
            GlobalConfiguration.Configuration.DependencyResolver = _DependencyResolver;

            #endregion

            #region Authetication providers

           // app.UseGSICommonAuthSettings();

            #endregion
        }
    }
}
