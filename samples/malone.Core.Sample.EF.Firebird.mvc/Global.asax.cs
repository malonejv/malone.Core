using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.DI;
using malone.Core.Sample.EF.Firebird.mvc;
using malone.Core.Unity;
using Microsoft.Owin.Security;
using Unity;

[assembly: PreApplicationStartMethod(typeof(App), nameof(App.PreStart))]

namespace malone.Core.Sample.EF.Firebird.mvc
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
