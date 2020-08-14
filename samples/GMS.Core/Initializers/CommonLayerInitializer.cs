using GMS.Core.CL.Exceptions;
using malone.Core.Commons.Initializers;
using malone.Core.Commons.Localization;
using Unity;

namespace GMS.Core.Initializers
{
    public class CommonLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<ILocalizationHandler<ErrorCodes>, LocalizationHandler<ErrorCodes>>();
        }
    }
}
