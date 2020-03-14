using malone.Core.DAL.EF.Context.Identity;
using malone.Core.EL.Identity;

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
