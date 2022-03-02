using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.Configuration.Features;
using Unity;

namespace malone.Core.Unity.ModulesInitializers
	{
	public class FeaturesModuleInitializer : IModuleInitializer<IUnityContainer>
    {
        public string Name => CoreModules.Features.GetDescription();

        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<FeatureSettings>();
            container.RegisterType<FeatureSettingsSection>();
            var featureSettings = container.Resolve<FeatureSettings>();
            container.RegisterInstance(featureSettings);
        }
    }
}
