using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.Identity;
using malone.Core.Identity.EntityFramework.Business;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity;

namespace malone.Core.Unity.IdentityEntityFramworkInitializer
{
    public class IdentityEntityFramworkModuleInitializer : IModuleInitializer<IUnityContainer>
    {
        public string Name => CoreModules.IdentityEntityFramework.GetDescription();

        public void Initialize(IUnityContainer container)
        {
            //Identity Entities
            container.RegisterType<CoreUser>();
            container.RegisterType<CoreRole>();
            container.RegisterType<CoreUserLogin>();
            container.RegisterType<CoreUserRole>();
            container.RegisterType<CoreUserClaim>();

            //IDENTITY STORES
            container.RegisterType<IRoleStore<CoreRole, int>, RoleStore<CoreRole, int, CoreUserRole>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserStore<CoreUser, CoreRole, int, CoreUserLogin, CoreUserRole, CoreUserClaim>>();

                        container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            container.RegisterType<IPasswordHasher, PasswordHasher>();
            container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();
        }
    }
}
