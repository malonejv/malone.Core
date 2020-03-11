using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.DAL.EF.Context.Identity
{
    internal class CoreUserConfiguration : DbEntityConfiguration<CoreUser>
    {
        public override void Configure(EntityTypeBuilder<CoreUser> entity)
        {
            entity.ToTable("Users");
            entity.Property(p => p.Id);
            entity.HasIndex((CoreUser u) => u.UserName).IsUnique();
        }
    }
}
