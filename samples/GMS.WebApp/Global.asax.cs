using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GMS.Core.Initializers;
using malone.Core.Commons.Initializers;
using malone.Core.Unity;
using Unity;

namespace GMS.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppInitializer<UnityMvcActivator, IUnityContainer, GMSInitializer>.Initialize();

        }
    }
}
