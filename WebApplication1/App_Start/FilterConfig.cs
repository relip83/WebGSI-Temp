using System.Web;
using System.Web.Mvc;

namespace Galcon.GSI.Systems.GSIGroup.WebServices.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
