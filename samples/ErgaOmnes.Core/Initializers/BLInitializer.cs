using ErgaOmnes.Core.BL;
using malone.Core.Commons.Initializers;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class BLInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<IEjemploBV, EjemploBV>();
            
            //BUSINESS COMPONENTS
            container.RegisterType<IEjemploBC, EjemploBC>();
        }
    }
}
