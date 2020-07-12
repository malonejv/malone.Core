using ErgaOmnes.Core.EL.Model;
using ErgaOmnes.Core.EL.RequestParams;
using malone.Core.Commons.Initializers;
using malone.Core.WebApi;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class ELInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<Ejemplo>();

            container.RegisterType<IGetRequestParam<Ejemplo>, EjemploGetRequestParams>();
        }
    }
}
