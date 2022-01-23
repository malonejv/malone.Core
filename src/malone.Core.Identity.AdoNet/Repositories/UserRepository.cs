using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.AdoNet.Entities;
using System.Data.Entity;
using malone.Core.Identity.AdoNet.Context;
using malone.Core.DataAccess.Context;

namespace malone.Core.DataAccess.EF.Repositories.Identity
{
    public class UserRepository<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TContext> : UserStore<TUserEntity, TRoleEntity, TKey, TUserLogin, TUserRole, TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TContext : EFIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>, IContext
    {
        public UserRepository(TContext context) : base(context)
        {
        }
    }

    public class UserRepository<TContext> : UserRepository<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim,TContext>
        where TContext : AdoNetIdentityDbContext, IContext
    {
        public UserRepository(TContext context) : base(context)
        {
        }
    }

}
