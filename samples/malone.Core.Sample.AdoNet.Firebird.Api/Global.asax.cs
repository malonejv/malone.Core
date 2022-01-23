using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.Firebird.Api;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: PreApplicationStartMethod(typeof(AppStart), nameof(AppStart.Start))]
namespace malone.Core.Sample.AdoNet.Firebird.Api
{
    public static class AppStart
    {
        public static void Start()
        {
        }
    }

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppInitializer<UnityWebApiActivator, IUnityContainer, SampleInitializer>.Initialize();
        }
    }
}
