using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.EF.Repositories.Identity;
using malone.Core.Identity;
using malone.Core.Identity.AdoNet;
using malone.Core.Identity.AdoNet.Business;
using malone.Core.Identity.AdoNet.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Unity;

namespace malone.Core.Unity.IdentityEntityFramworkInitializer
{
    public class IdentityAdoNetModuleInitializer : IModuleInitializer<IUnityContainer>
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
            
            //IDENTITY REPOSITORIES
            container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            ////IDENTITY SERVICES
            container.RegisterType<UserManager<CoreUser, int>, UserBusinessComponent>();
            container.RegisterType<RoleManager<CoreRole, int>, RoleBusinessComponent>();
            container.RegisterType<SignInManager<CoreUser, int>, SignInBusinessComponent>();
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();
            container.RegisterType<IPasswordHasher, PasswordHasher>();

        }
    }
}
