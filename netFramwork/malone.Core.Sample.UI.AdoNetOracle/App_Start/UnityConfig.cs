using System;
using AutoMapper;
using malone.core.Sample.DI;
using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.CoreConfiguration;
using malone.Core.CL.Configurations.Sections;
using malone.Core.CL.Configurations.Sections.Feature;
using Unity;

namespace malone.Core.Sample.UI.AdoNetOracle
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            var mapperConfiguration = AutoMapperConfig.RegisterProfiles();
            var mapper = new Mapper(mapperConfiguration);
            container.RegisterInstance(mapper);

            container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            var featureSettings = container.Resolve<FeatureSettings>();
            container.RegisterInstance(featureSettings);

            // TODO: Register your type's mappings here.
            RegisterEntitesLayer.RegisterTypes(container);
            RegisterCommonTypes.RegisterTypes(container);
            RegisterDataAccessLayerTypes.RegisterTypes(container);
            RegisterBusinessLayerTypes.RegisterTypes(container);
            RegisterViewLayerTypes.RegisterTypes(container);
        }
    }
}