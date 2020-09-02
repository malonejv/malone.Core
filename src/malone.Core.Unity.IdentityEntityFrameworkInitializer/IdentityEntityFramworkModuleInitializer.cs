using System.Web;
using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.EF.Repositories.Identity;
using malone.Core.Identity;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Business;
using malone.Core.Identity.EntityFramework.Context;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Unity;
using Unity.Injection;

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

            //IDENTITY REPOSITORIES
            container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            ////IDENTITY SERVICES
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            container.RegisterType<IPasswordHasher, PasswordHasher>();
            container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();
        }
    }
}
