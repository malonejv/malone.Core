using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
	public class UserLoginGetRequest<TKey> : IFilterExpressionAdoNet
		where TKey : IEquatable<TKey>
	{
		[StringLength(100)]
		[Column("@LoginProvider", Type = DbType.String, Order = 1, Direction = ParameterDirection.Input)]
		public string LoginProvider { get; set; }

		[StringLength(100)]
		[Column("@ProviderKey", Type = DbType.String, Order = 2, Direction = ParameterDirection.Input)]
		public string ProviderKey { get; set; }

		//TODO: Esto esta condicionando a usar int
		[Column("@UserId", Type = DbType.Int32, Order = 3, Direction = ParameterDirection.Input)]
		public TKey UserId { get; set; }
	}
}
