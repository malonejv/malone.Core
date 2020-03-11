using malone.Core.DAL.EF.Context.Identity;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace malone.Core.DAL.EF.Repositories.Identity
{
    public class RoleRepository : RoleRepository<CoreRole,EFIdentityDbContext> {

        public RoleRepository(EFIdentityDbContext context) : base(context)
        {
        }

    }

    public class RoleRepository<TRole, TContext> : RoleStore<TRole, TContext, int>
        where TRole : CoreRole
        where TContext : EFIdentityDbContext
    {
        public RoleRepository(TContext context) : base(context)
        {
        }
    }
}
