using malone.Core.Identity.EntityFramework.EL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.DAL.Mappings
{
    public class CoreUserRoleMapping<TKey, TUserRole> : EntityTypeConfiguration<TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {
        public CoreUserRoleMapping()
        {
            ToTable("UsersRoles")
                .HasKey((TUserRole r) => new
                {
                    r.UserId,
                    r.RoleId
                });

        }
    }
}
