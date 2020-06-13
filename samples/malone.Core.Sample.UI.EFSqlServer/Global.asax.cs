using malone.Core.Commons.Initializers;
using malone.Core.Sample.DI;
using malone.Core.Unity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace malone.Core.Sample.UI.EFSqlServer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppInitializer<UnityMvcActivator, IUnityContainer, SampleInitializer>.Initialize();

        }
    }
}
