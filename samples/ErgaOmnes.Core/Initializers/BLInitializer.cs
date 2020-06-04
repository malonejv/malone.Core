using ErgaOmnes.Core.BL;
using malone.Core.CL.Initializers;
using malone.Core.Identity.BL.Components.MessageServices;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.BL;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class BLInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<IEjemploBV, EjemploBV>();
            
            //BUSINESS COMPONENTS
            container.RegisterType<IEjemploBC, EjemploBC>();


            //IDENTITY SERVICES
            container.RegisterType<UserManager<CoreUser, int>, UserBusinessComponent>();
            container.RegisterType<RoleManager<CoreRole, int>, RoleBusinessComponent>();
            container.RegisterType<SignInManager<CoreUser, int>, SignInBusinessComponent>();
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();
        }
    }
}
