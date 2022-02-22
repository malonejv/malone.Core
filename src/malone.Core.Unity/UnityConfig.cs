using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Commons.Configurations;
using malone.Core.Commons.Configurations.Modules;
using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.Unity.IdentityAdoNetSqlServerInitializer;
using malone.Core.Unity.IdentityDapperInitializer;
using malone.Core.Unity.IdentityEntityFramworkInitializer;
using malone.Core.Unity.Log4NetInitializer;
using malone.Core.Unity.ModulesInitializers;
using Unity;
using Unity.Injection;

namespace malone.Core.Unity
	{
	public class UnityConfig
		{
		#region Unity Container Static

		private static Lazy<IUnityContainer> container =
		  new Lazy<IUnityContainer>(() => {
			  UnityContainer container = new UnityContainer();
#if DEBUG
			  container.AddExtension(new Diagnostic());
#endif

			  //Core Service Locator
			  container.RegisterType<IServiceLocator, UnityServiceLocator>(new InjectionConstructor(container));
			  var unityServiceLocator = container.Resolve<IServiceLocator>();
			  ServiceLocator.SetResolver(unityServiceLocator);

			  RegisterCoreTypes(container);

			  //DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

			  return container;
		  });

		public static IUnityContainer Container => container.Value;

		#endregion

		public UnityConfig()
			{
			}

		public static void RegisterCoreTypes(IUnityContainer container)
			{
			//Inicialización de módulos base del core
			var basicModules = new BasicModulesInitializer();
			basicModules.Initialize(container);

			var coreConfiguration = container.Resolve<ICoreConfiguration>();
			var coreSettings = coreConfiguration.GetSection<CoreSettingsSection>();
			if (coreSettings != null)
				{
				//Inicialización de módulos configurables
				foreach (ModuleElement module in coreSettings.Modules)
					{
					var moduleInitializer = Modules.Where(m => m.Name == module.Name).FirstOrDefault();
					if (moduleInitializer != null)
						{
						moduleInitializer.Initialize(container);
						}
					}
				}
			}

		private static IEnumerable<IModuleInitializer<IUnityContainer>> Modules
			{
			get
				{
				return new IModuleInitializer<IUnityContainer>[]
			  {
					new Log4NetModuleInitializer(),
					new FeaturesModuleInitializer(),
					new IdentityEntityFramworkModuleInitializer(),
					new IdentityAdoNetSqlServerModuleInitializer(),
					new IdentityDapperModuleInitializer()
			  };
				}
			}
		}
	}
