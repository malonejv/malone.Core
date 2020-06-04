using malone.Core.CL.DI.Unity;
using malone.Core.CL.Initializers;
using malone.Core.Sample.DI;
using malone.Core.Sample.mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

[assembly: PreApplicationStartMethod(typeof(App), nameof(App.PreStart))]

namespace malone.Core.Sample.mvc
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
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppInitializer<UnityMvcActivator, IUnityContainer, SampleInitializer>.Initialize();

        }
    }

}
