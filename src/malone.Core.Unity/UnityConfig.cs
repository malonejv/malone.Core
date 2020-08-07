using malone.Core.Commons.Configurations;
using malone.Core.Commons.Configurations.Modules;
using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Unity.IdentityEntityFramworkInitializer;
using malone.Core.Unity.Log4NetInitializer;
using malone.Core.Unity.ModulesInitializers;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace malone.Core.Unity
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

              //Core Service Locator
              container.RegisterType<IServiceLocator, UnityServiceLocator>(new InjectionConstructor(container));
              var unityServiceLocator = container.Resolve<IServiceLocator>();
              ServiceLocator.SetResolver(unityServiceLocator);

              RegisterCoreTypes(container);

              ////OPTION: Uncomment if you want to use PerRequestLifetimeManager
              //DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

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
                    new IdentityEntityFramworkModuleInitializer()
              };
            }
        }
    }
}
