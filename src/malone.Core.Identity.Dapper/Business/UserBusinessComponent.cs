using System;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.IoC;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace malone.Core.Identity.Dapper.Business
{

	public class UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : UserManager<TUserEntity, TKey>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>, new()
		where TUserRole : CoreUserRole<TKey>, new()
		where TUserClaim : CoreUserClaim<TKey>, new()
		where TRoleEntity : CoreRole<TKey, TUserRole>
		where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
	{
		public UserService(IUserStore<TUserEntity, TKey> store) : base(store)
		{
		}

		public static UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> Create(IdentityFactoryOptions<UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>> options, IOwinContext context)
		{
			var instance = ServiceLocator.Current.Get<UserManager<TUserEntity, TKey>>() as UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>;
			return instance;
		}

		public static UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> Create(IdentityFactoryOptions<UserService> options, IOwinContext context)
		{
			var store = ServiceLocator.Current.Get<IUserStore<TUserEntity, TKey>>();
			var userService = new UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>(store);
			//var userService = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserService;

			var dataProtectionProvider = options.DataProtectionProvider;

			if (dataProtectionProvider != null)
			{
				userService.UserTokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(dataProtectionProvider.Create("DataProtectorToken"));
			}

			return userService;
		}


		public static UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> CreateAndConfigure(IdentityFactoryOptions<UserService> options, IOwinContext context)
		{

			var store = ServiceLocator.Current.Get<IUserStore<TUserEntity, TKey>>();
			var userService = new UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>(store);
			var userServiceConfiguration = ServiceLocator.Current.Get<IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>>>();

			userServiceConfiguration.ConfigureUserManager(userService, options);

			return userService;

		}

	}

	public class UserService : UserService<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
	{
		public UserService(IUserStore<CoreUser, int> store) : base(store)
		{
		}


		public static new UserService Create(IdentityFactoryOptions<UserService> options, IOwinContext context)
		{
			var store = ServiceLocator.Current.Get<IUserStore<CoreUser, int>>();
			var userService = new UserService(store);
			//var userService = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserService;

			var dataProtectionProvider = options.DataProtectionProvider;

			if (dataProtectionProvider != null)
			{
				userService.UserTokenProvider = new DataProtectorTokenProvider<CoreUser, int>(dataProtectionProvider.Create("DataProtectorToken"));
			}

			return userService;
		}


		public static new UserService CreateAndConfigure(IdentityFactoryOptions<UserService> options, IOwinContext context)
		{
			var store = ServiceLocator.Current.Get<IUserStore<CoreUser, int>>();
			var userServiceConfiguration = ServiceLocator.Current.Get<IUserManagerConfiguration>();

			var userService = new UserService(store);
			userServiceConfiguration.ConfigureUserManager(userService, options);

			return userService;
		}

	}

}
