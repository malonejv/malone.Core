using ErgaOmnes.Core.EL.Model;
using malone.Core.Commons.Initializers;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class ELInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<Ejemplo>();
        }
    }
}
