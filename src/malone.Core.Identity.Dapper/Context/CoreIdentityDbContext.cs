using System;
using malone.Core.AdoNet.Context;
using malone.Core.Commons.DI;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;

namespace malone.Core.Identity.Dapper.Context
{
    public class CoreIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
        : CoreDbContext, IContext
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {

        public static CoreIdentityDbContext Create()
        {
            var instance = ServiceLocator.Current.Get<IContext>() as CoreIdentityDbContext;
            return instance;
        }

        public CoreIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
        {
        }

    }

    public class CoreIdentityDbContext : CoreIdentityDbContext<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {

        public CoreIdentityDbContext(string nameOrConnectionStringName)
            : base(nameOrConnectionStringName)
        {

        }

    }

}
