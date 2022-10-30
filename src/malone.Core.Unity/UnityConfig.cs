using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using malone.Core.Configuration;
using malone.Core.Configuration.Modules;
using malone.Core.IoC;
using malone.Core.Unity.ModulesInitializers;
using Unity;
using Unity.Injection;

namespace malone.Core.Unity
{
	public class UnityConfig
	{
		private const string INITIALIZE_METHOD = "Initialize";

		#region Unity Container Static

		private static Lazy<IUnityContainer> container =
		  new Lazy<IUnityContainer>(() =>
		  {
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
					var type = module.Type.Split(',');
					var typeDescription = type.First().Trim();
					var assemblyDescription = type.Last().Trim();
					var assemblyInfo = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyDescription);
					if (assemblyInfo != null)
					{
						Type typeInfo = assemblyInfo.GetType(typeDescription);
						object instance = Activator.CreateInstance(typeInfo);
						MethodInfo method = typeInfo.GetMethod(INITIALIZE_METHOD);
						method.Invoke(instance, new object[] { container });
					}
					else
					{
						throw new ConfigurationException($"Could not load section {module.Type}");
					}
				}
			}
		}
	}
}
