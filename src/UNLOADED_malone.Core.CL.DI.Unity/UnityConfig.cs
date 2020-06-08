using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.Modules;
using System;
using System.Configuration;
using Unity;
using Unity.Injection;

namespace malone.Core.CL.DI.Unity
{
    public class UnityConfig
    {
        #region Unity Container Static

        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              UnityContainer container = new UnityContainer();
#if DEBUG
              container.AddExtension(new Diagnostic());
#endif
              RegisterCoreTypes(container);
              //TODO: Integrar en el framework
              container.RegisterType<IServiceLocator, UnityServiceLocator>(new InjectionConstructor(container));
              var unityServiceLocator = container.Resolve<IServiceLocator>();

              ////OPTION: Uncomment if you want to use PerRequestLifetimeManager
              //DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

              //Core Service Locator
              ServiceLocator.SetResolver(unityServiceLocator);
              
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        #endregion

        public UnityConfig()
        {
        }

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterCoreTypes(IUnityContainer container)
        {
            CoreConfiguration coreConfiguration = new CoreConfiguration();
            var coreSettings = coreConfiguration.GetSection<CoreSettingsSection>();
            if (coreSettings == null) throw new ConfigurationErrorsException(nameof(coreConfiguration));

            foreach (ModuleElement module in coreSettings.Modules)
            {
                if (module.Enabled)
                {

                }
            }
        }
    }
}
