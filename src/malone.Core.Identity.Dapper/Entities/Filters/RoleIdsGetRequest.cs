using System;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
	public class RoleIdsGetRequest<TKey> : IFilterExpressionDapper
		where TKey : IEquatable<TKey>
	{
		[Parameter(name: "@Ids", Direction = ParameterDirection.Input)]
		public TKey[] Ids { get; set; }

	}
}
