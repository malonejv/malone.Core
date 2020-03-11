using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.DAL.EF.Context.Identity
{
    internal class CoreUserRolesConfiguration : DbEntityConfiguration<CoreUserRole>
    {
        public override void Configure(EntityTypeBuilder<CoreUserRole> entity)
        {
            entity.ToTable("UsersRoles")
                .HasKey((CoreUserRole r) => new
                {
                    r.UserId,
                    r.RoleId
                });
        }
    }
}
