using ErgaOmnes.Core.EL.Model;
using malone.Core.CL.Initializers;
using malone.Core.Identity.EntityFramework.EL;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class ELInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<Ejemplo>();

            //Identity Entities
            container.RegisterType<CoreUser>();
            container.RegisterType<CoreRole>();
            container.RegisterType<CoreUserLogin>();
            container.RegisterType<CoreUserRole>();
            container.RegisterType<CoreUserClaim>();
        }
    }
}
