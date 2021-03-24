using malone.Core.Identity.EntityFramework.Entities;
using System;
using System.Data.Entity.ModelConfiguration;

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
