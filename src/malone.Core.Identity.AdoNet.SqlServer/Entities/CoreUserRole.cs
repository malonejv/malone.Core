using System;
using System.Data;
using malone.Core.AdoNet.Attributes;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
	public class CoreUserRole<TKey>
		where TKey : IEquatable<TKey>
	{
		[Column("@UserId", Type = DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey UserId { get; set; }

		[Column("@RoleId", Type = DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey RoleId { get; set; }

	}

	public class CoreUserRole : CoreUserRole<int>
	{
		public CoreUserRole() : base() { }

	}
}
