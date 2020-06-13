using malone.Core.CL.Initializers;
using System.Linq;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace malone.Core.CL.DI.Unity
{
    public class UnityMvcActivator : IInjectorInitializer<IUnityContainer>
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public virtual IUnityContainer Initialize()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            //Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

            return UnityConfig.Container;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public virtual void Terminate()
        {
            UnityConfig.Container.Dispose();
        }
    }
}