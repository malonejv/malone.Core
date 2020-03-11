using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.DAL.EF.Context.Identity
{
    internal class CoreUserLoginsConfiguration : DbEntityConfiguration<CoreUserLogin>
    {
        public override void Configure(EntityTypeBuilder<CoreUserLogin> entity)
        {
            entity.ToTable("UsersLogins")
                .HasKey((CoreUserLogin l) => new
                {
                    l.LoginProvider,
                    l.ProviderKey,
                    l.UserId
                });
        }
    }
}
