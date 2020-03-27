using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.DAL.EF.Context.Identity
{
    internal class CoreRoleConfiguration : DbEntityConfiguration<CoreRole>
    {
        public override void Configure(EntityTypeBuilder<CoreRole> entity)
        {
            entity.ToTable("Roles");
            entity.HasIndex((CoreRole r) => r.Name).IsUnique();
        }

    }
}
