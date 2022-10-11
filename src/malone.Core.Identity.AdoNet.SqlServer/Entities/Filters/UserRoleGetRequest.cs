﻿using System;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
	public class UserRoleGetRequest<TKey> : IFilterExpressionAdoNet
		where TKey : IEquatable<TKey>
	{
		//TODO: Esto esta condicionando a usar int
		[Column("@UserId", Type = DbType.Int32, Order = 1, Direction = ParameterDirection.Input)]
		public TKey UserId { get; set; }

		//TODO: Esto esta condicionando a usar int
		[Column("@RoleId", Type = DbType.Int32, Order = 2, Direction = ParameterDirection.Input)]
		public TKey RoleId { get; set; }
	}
}
