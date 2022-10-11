using System.Linq;
using System.Web.Mvc;
using malone.Core.Commons.Initializers;
using Unity;
using Unity.AspNet.Mvc;

namespace malone.Core.Unity
{
	public class UnityMvcActivator : IInjectorInitializer<IUnityContainer>
	{
		public virtual IUnityContainer Initialize()
		{
			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
			FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

			DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

			// TODO: Uncomment if you want to use PerRequestLifetimeManager
			//Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

			return UnityConfig.Container;
		}

		public virtual void Terminate()
		{
			UnityConfig.Container.Dispose();
		}
	}
}