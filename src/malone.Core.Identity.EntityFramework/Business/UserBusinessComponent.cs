using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace malone.Core.Identity.EntityFramework
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

    }

    public class UserBusinessComponent : UserBusinessComponent<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public UserBusinessComponent(IUserStore<CoreUser, int> store) : base(store)
        {
        }

        public static UserBusinessComponent Create(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {
            var instance = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;
            return instance;
        }

    }

}
