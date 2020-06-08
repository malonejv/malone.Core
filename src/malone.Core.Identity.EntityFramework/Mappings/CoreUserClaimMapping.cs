using malone.Core.Identity.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.DAL.Mappings
{
    public class CoreUserClaimMapping<TKey, TUserClaim> : EntityTypeConfiguration<TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>
    {
        public CoreUserClaimMapping()
        {
            ToTable("UsersClaims");
        }
    }
}
