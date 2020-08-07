using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.CL.Localization;
using malone.Core.Commons.Initializers;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class CLInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IErrorLocalizationHandler, ErrorLocalizationHandler>();
            container.RegisterType<IContentLocalizationHandler, ContentLocalizationHandler>();


        }
    }
}
