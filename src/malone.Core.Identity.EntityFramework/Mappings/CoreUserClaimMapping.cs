using System;
using System.Data.Entity.ModelConfiguration;
using malone.Core.Identity.EntityFramework.Entities;

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
