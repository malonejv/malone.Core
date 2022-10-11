using System;
using System.Data;
using malone.Core.Dapper.Attributes;

namespace malone.Core.Identity.Dapper.Entities
{
	[Table("UsersLogins")]
	public class CoreUserLogin<TKey>
		where TKey : IEquatable<TKey>
	{
		[Column("LoginProvider", type: DbType.String, Size = 128, Direction = ParameterDirection.Input)]
		public virtual string LoginProvider { get; set; }

		[Column("ProviderKey", type: DbType.String, Size = 128, Direction = ParameterDirection.Input)]
		public virtual string ProviderKey { get; set; }

		[Column("UserId", type: DbType.Int32, Direction = ParameterDirection.Input)]
		public virtual TKey UserId { get; set; }
	}

	public class CoreUserLogin : CoreUserLogin<int>
	{

	}
}
