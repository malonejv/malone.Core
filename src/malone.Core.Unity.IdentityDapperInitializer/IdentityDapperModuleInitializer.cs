using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.Identity;
using malone.Core.Identity.Dapper.Business;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.Identity.Dapper.Repositories;
using Microsoft.AspNet.Identity;
using Unity;

namespace malone.Core.Unity.IdentityDapperInitializer
{
	public class IdentityDapperModuleInitializer : IModuleInitializer<IUnityContainer>
	{
		public string Name => CoreModules.IdentityDapper.GetDescription();

		public void Initialize(IUnityContainer container)
		{
			//Identity Entities
			container.RegisterType<CoreUser>();
			container.RegisterType<CoreRole>();
			container.RegisterType<CoreUserLogin>();
			container.RegisterType<CoreUserRole>();
			container.RegisterType<CoreUserClaim>();

			//IDENTITY REPOSITORIES
			container.RegisterType<IUserLoginRepository<CoreUserLogin>, UserLoginRepository<CoreUserLogin>>();
			container.RegisterType<IUserClaimRepository<CoreUserClaim>, UserClaimRepository<CoreUserClaim>>();
			container.RegisterType<IUserRoleRepository<CoreUserRole>, UserRoleRepository<CoreUserRole>>();
			container.RegisterType<IRoleRepository<CoreRole>, RoleRepository<CoreRole, CoreUserRole>>();
			container.RegisterType<IUserRepository<CoreUser>, UserRepository<CoreUser, CoreUserLogin, CoreUserRole, CoreUserClaim>>();
			container.RegisterType<IRoleStore<CoreRole, int>, RoleStore<CoreRole, CoreUserRole>>();
			container.RegisterType<IUserStore<CoreUser, int>, UserStore<CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>>();

			container.RegisterType<IEmailMessageService, EmailService>();
			container.RegisterType<ISmsMessageService, SmsService>();

			container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
			container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
			container.RegisterType<IPasswordHasher, PasswordHasher>();
			container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();
		}
	}
}
