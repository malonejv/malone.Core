using malone.Core.DAL.EF.Context.Identity;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace malone.Core.DAL.EF.Repositories.Identity
{
    public class UserRepository : UserRepository<CoreUser, CoreRole, EFIdentityDbContext>
    {
        public UserRepository(EFIdentityDbContext context) : base(context)
        {
        }
    }

    public class UserRepository<TUserEntity, TRoleEntity, TContext> : UserStore<CoreUser,CoreRole,EFIdentityDbContext,int,CoreUserClaim,CoreUserRole,CoreUserLogin,CoreUserToken,CoreRoleClaim>
        where TUserEntity : CoreUser
        where TRoleEntity : CoreRole
        where TContext : EFIdentityDbContext
    {
        public UserRepository(TContext context) : base(context)
        {
        }
    }
}
