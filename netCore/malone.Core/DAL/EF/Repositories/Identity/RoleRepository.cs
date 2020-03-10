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
