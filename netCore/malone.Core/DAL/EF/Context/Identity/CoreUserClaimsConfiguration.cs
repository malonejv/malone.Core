using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.DAL.EF.Context.Identity
{
    internal class CoreUserClaimsConfiguration : DbEntityConfiguration<CoreUserClaim>
    {
        public override void Configure(EntityTypeBuilder<CoreUserClaim> entity)
        {
            entity.ToTable("UsersClaims");
        }
    }
}
