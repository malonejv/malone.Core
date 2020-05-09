using malone.Core.CL.DI.Unity;
using malone.Core.Sample.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityMvcActivator<SampleBootstrapper>), nameof(UnityMvcActivator<SampleBootstrapper>.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityMvcActivator<SampleBootstrapper>), nameof(UnityMvcActivator<SampleBootstrapper>.Shutdown))]

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
        }
    }
}
