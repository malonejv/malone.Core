using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.EntityFramework.EL;
using System.Data.Entity;
using malone.Core.Identity.EntityFramework.DAL.EF.Context;
using malone.Core.DAL.Context;

namespace malone.Core.DAL.EF.Repositories.Identity
{

    public class RoleRepository<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TContext> : RoleStore<TRoleEntity, TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
        where TContext : EFIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>, IContext
    {
        public RoleRepository(TContext context) : base(context)
        {
        }
    }

    public class RoleRepository<TContext> : RoleRepository<int, CoreUser, CoreRole, CoreUserLogin,CoreUserRole,CoreUserClaim, EFIdentityDbContext>
        where TContext : EFIdentityDbContext, IContext
    {

        public RoleRepository(TContext context) : base(context)
        {
        }

    }

}
