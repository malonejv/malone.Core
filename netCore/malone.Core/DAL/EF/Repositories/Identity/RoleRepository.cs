using malone.Core.DAL.EF.Context.Identity;
using malone.Core.EL.Identity;

namespace malone.Core.DAL.EF.Repositories.Identity
{
    public class RoleRepository : RoleRepository<CoreRole> {

        public RoleRepository(EFIdentityDbContext context) : base(context)
        {
        }

    }

    public class RoleRepository<TRoleEntity> : RoleStore<TRoleEntity, int, CoreUserRole>
        where TRoleEntity : CoreRole, new()
    {
        public RoleRepository(EFIdentityDbContext context) : base(context)
        {
        }
    }
}
