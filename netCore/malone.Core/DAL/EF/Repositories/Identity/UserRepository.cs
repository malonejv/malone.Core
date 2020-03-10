using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL.Identity;
using System.Data.Entity;
using malone.Core.DAL.EF.Context.Identity;

namespace malone.Core.DAL.EF.Repositories.Identity
{
    public class UserRepository : UserRepository<CoreUser, CoreRole>
    {
        public UserRepository(EFIdentityDbContext context) : base(context)
        {
        }
    }

    public class UserRepository<TUserEntity, TRoleEntity> : UserStore<TUserEntity, TRoleEntity, int, CoreUserLogin, CoreUserRole, CoreUserClaim>
        where TUserEntity : CoreUser
        where TRoleEntity : CoreRole
    {
        public UserRepository(EFIdentityDbContext<TUserEntity,TRoleEntity> context) : base(context)
        {
        }
    }
}
