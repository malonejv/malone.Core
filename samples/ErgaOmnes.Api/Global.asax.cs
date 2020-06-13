using ErgaOmnes.Api;
using ErgaOmnes.Core.Initializers;
using malone.Core.Commons.Initializers;
using malone.Core.Unity.WebApi;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

[assembly: PreApplicationStartMethod(typeof(AppStart), nameof(AppStart.Start))]
namespace ErgaOmnes.Api
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
            AppInitializer<UnityWebApiActivator, IUnityContainer, ErgaOmnesInitializer>.Initialize();
        }
    }
}
