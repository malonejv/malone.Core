using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.EntityFramework.EL;
using System.Data.Entity;
using malone.Core.Identity.EntityFramework.DAL.EF.Context;

namespace malone.Core.DAL.EF.Repositories.Identity
{
    public class UserRepository<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : UserStore<TUserEntity, TRoleEntity, TKey, TUserLogin, TUserRole, TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public UserRepository(EFIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> context) : base(context)
        {
        }
    }

    public class UserRepository : UserRepository<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public UserRepository(EFIdentityDbContext context) : base(context)
        {
        }
    }

}
