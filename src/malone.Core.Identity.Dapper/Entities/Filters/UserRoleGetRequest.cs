using System;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class UserRoleGetRequest<TKey> : IFilterExpressionDapper
        where TKey : IEquatable<TKey>
    {
        //TODO: Esto esta condicionando a usar int
        [Parameter("@UserId", type: DbType.Int32, Order = 1, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }

        //TODO: Esto esta condicionando a usar int
        [Parameter("@RoleId", type: DbType.Int32, Order = 2, Direction = ParameterDirection.Input)]
        public TKey RoleId { get; set; }
    }
}
