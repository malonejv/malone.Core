using malone.Core.Commons.DI;
using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace malone.Core.Identity.Dapper.Business
{

    public class UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : UserManager<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public UserBusinessComponent(IUserStore<TUserEntity, TKey> store) : base(store)
        {
        }

        public static UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> Create(IdentityFactoryOptions<UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>> options, IOwinContext context)
        {
            var instance = ServiceLocator.Current.Get<UserManager<TUserEntity, TKey>>() as UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>;
            return instance;
        }

        public static UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> Create(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {
            var store = ServiceLocator.Current.Get<IUserStore<TUserEntity, TKey>>();
            var userBusinessComponent = new UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>(store);
            //var userBusinessComponent = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                userBusinessComponent.UserTokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(dataProtectionProvider.Create("DataProtectorToken"));
            }

            return userBusinessComponent;
        }


        public static UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> CreateAndConfigure(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {

            var store = ServiceLocator.Current.Get<IUserStore<TUserEntity, TKey>>();
            var userBusinessComponent = new UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>(store);
            var userBusinessComponentConfiguration = ServiceLocator.Current.Get<IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>>>();

            userBusinessComponentConfiguration.ConfigureUserManager(userBusinessComponent, options);

            return userBusinessComponent;

        }

    }

    public class UserBusinessComponent : UserBusinessComponent<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public UserBusinessComponent(IUserStore<CoreUser, int> store) : base(store)
        {
        }


        public static new UserBusinessComponent Create(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {
            var store = ServiceLocator.Current.Get<IUserStore<CoreUser, int>>();
            var userBusinessComponent = new UserBusinessComponent(store);
            //var userBusinessComponent = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                userBusinessComponent.UserTokenProvider = new DataProtectorTokenProvider<CoreUser, int>(dataProtectionProvider.Create("DataProtectorToken"));
            }

            return userBusinessComponent;
        }


        public static new UserBusinessComponent CreateAndConfigure(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {
            var store = ServiceLocator.Current.Get<IUserStore<CoreUser, int>>();
            var userBusinessComponentConfiguration = ServiceLocator.Current.Get<IUserManagerConfiguration>();

            var userBusinessComponent = new UserBusinessComponent(store);
            userBusinessComponentConfiguration.ConfigureUserManager(userBusinessComponent, options);

            return userBusinessComponent;
        }

    }

}
