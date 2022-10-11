using System;
using System.Data;
using malone.Core.AdoNet.Attributes;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
	public class CoreUserLogin<TKey>
		where TKey : IEquatable<TKey>
	{
		[Column("@LoginProvider", Type = DbType.String, Size = 128, Direction = ParameterDirection.Input)]
		public virtual string LoginProvider { get; set; }

		[Column("@ProviderKey", Type = DbType.String, Size = 128, Direction = ParameterDirection.Input)]
		public virtual string ProviderKey { get; set; }

		[Column("@UserId", Type = DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey UserId { get; set; }
	}

	public class CoreUserLogin : CoreUserLogin<int>
	{

	}
}
