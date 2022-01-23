using malone.Core.CL.Initializers;
using System.Linq;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(malone.Core.Sample.mvc.UnityMvcActivator), nameof(malone.Core.Sample.mvc.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(malone.Core.Sample.mvc.UnityMvcActivator), nameof(malone.Core.Sample.mvc.UnityMvcActivator.Shutdown))]

namespace malone.Core.Sample.mvc
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public class UnityMvcActivator : IInjectorInitializer<IUnityContainer>
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public IUnityContainer Initialize()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

            return UnityConfig.Container;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public void Terminate()
        {
            UnityConfig.Container.Dispose();
        }
    }
}