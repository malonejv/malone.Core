using malone.Core.Identity.EntityFramework.BL;
using malone.Core.Patterns.Behavioral.Singleton;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.CL.DI.Unity
{
    public class UnityConfig<TRegistrator>
        where TRegistrator : CoreBootstrapper<IUnityContainer, TRegistrator>
    {
        #region Unity Container Static

        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);

              //TODO: Integrar en el framework
              container.RegisterType<IServiceLocator, UnityServiceLocator>(new InjectionConstructor(container));
              var unityServiceLocator = container.Resolve<IServiceLocator>();

              ServiceLocator.SetResolver(unityServiceLocator);
              DependencyResolver.SetResolver(new UnityDependencyResolver(container));

              //OPTION: Uncomment if you want to use PerRequestLifetimeManager
              DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

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

        public TRegistrator Registrator { get; set; }


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
        public static void RegisterTypes(IUnityContainer container)
        {

            //// NOTE: To load from web.config uncomment the line below.
            //// Make sure to add a Unity.Configuration to the using statements.
            //// container.LoadConfiguration();


            var coreRegistration = CoreBootstrapper<IUnityContainer, TRegistrator>.Instance;
            coreRegistration.SetUp(container);


            //#region View Layer Configuration

            //container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();

            ////var mapperConfiguration = AutoMapperConfig.RegisterProfiles();
            ////var mapper = new Mapper(mapperConfiguration);
            ////container.RegisterInstance(mapper);

            //container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            //container.RegisterType<FeatureSettings>();

            //var featureSettings = container.Resolve<FeatureSettings>();
            //container.RegisterInstance(featureSettings);
            //#endregion

            //RegisterEntitesLayer.RegisterTypes(container);
            //RegisterCommonTypes.RegisterTypes(container);
            //RegisterDataAccessLayerTypes.RegisterTypes(container);
            //RegisterBusinessLayerTypes.RegisterTypes(container);
        }
    }
}
