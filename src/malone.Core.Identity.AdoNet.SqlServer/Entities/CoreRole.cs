using System;
using System.Collections.Generic;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
	public class CoreRole<TKey, TUserRole> : IRole<TKey>, IBaseEntity<TKey>
		where TKey : IEquatable<TKey>
		where TUserRole : CoreUserRole<TKey>
	{
		public CoreRole()
		{
			Users = new List<TUserRole>();
		}

		[Column("@Id", Type = DbType.Int32, Direction = ParameterDirection.Input)]
		public TKey Id { get; set; }

		[Column("@Name", Type = DbType.String, Size = 256, Direction = ParameterDirection.Input)]
		public string Name { get; set; }

		public virtual ICollection<TUserRole> Users { get; internal set; }

	}


	public class CoreRole<TUserRole> : CoreRole<int, TUserRole>, IBaseEntity
		where TUserRole : CoreUserRole
	{
		public CoreRole()
		{
			Id = this.GetHashCode();
		}
	}

	public class CoreRole : CoreRole<CoreUserRole>
	{ }
}
