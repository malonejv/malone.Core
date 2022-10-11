using System;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Identity.EntityFramework.Business
{
	public interface IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>, new()
		where TUserRole : CoreUserRole<TKey>, new()
		where TUserClaim : CoreUserClaim<TKey>, new()
		where TRoleEntity : CoreRole<TKey, TUserRole>
		where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
		where TUserBC : UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
	{
		IEmailMessageService EmailService { get; set; }
		ISmsMessageService SmsService { get; set; }

		void ConfigureUserManager(TUserBC userService, IdentityFactoryOptions<UserService> options);
	}

	public interface IUserManagerConfiguration : IUserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserService>
	{
	}
}
