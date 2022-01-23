using malone.Core.Commons.Initializers;
using malone.Core.Commons.Localization;
using malone.Core.Sample.AdoNet.SqlServer.Middle.CL.Exceptions;
using malone.Core.Sample.AdoNet.SqlServer.Middle.CL.Localization;
using Unity;

namespace malone.Core.Sample.AdoNet.SqlServer.DI
{
    public class CommonLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<ILocalizationHandler<ErrorCode>, LocalizationHandler<ErrorCode>>();
            container.RegisterType<IErrorLocalizationHandler, ErrorLocalizationHandler>();
            container.RegisterType<IContentLocalizationHandler, ContentLocalizationHandler>();
        }
    }
}
