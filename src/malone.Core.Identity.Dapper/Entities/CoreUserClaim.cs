using System;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.Dapper.Entities
{
	[Table("UsersClaims")]
	public class CoreUserClaim<TKey> : IBaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		[Column("Id", type: DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey Id { get; set; }

		[Column("UserId", type: DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey UserId { get; set; }

		[Column("ClaimType", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
		public virtual string ClaimType { get; set; }

		[Column("ClaimValue", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
		public virtual string ClaimValue { get; set; }

	}

	public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
	{
		public CoreUserClaim() : base() { }
	}
}
