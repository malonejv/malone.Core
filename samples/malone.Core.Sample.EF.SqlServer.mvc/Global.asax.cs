using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.EF.SqlServer.DI;
using malone.Core.Sample.EF.SqlServer.mvc;
using malone.Core.Unity;
using Unity;

[assembly: PreApplicationStartMethod(typeof(App), nameof(App.PreStart))]

namespace malone.Core.Sample.EF.SqlServer.mvc
{
    public static class App
    {
        public static void PreStart()
        {

        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppInitializer<UnityMvcActivator, IUnityContainer, SampleInitializer>.Initialize();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

        }
    }

}
