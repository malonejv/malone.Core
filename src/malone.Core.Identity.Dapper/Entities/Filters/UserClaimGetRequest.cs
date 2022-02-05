using System;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class UserClaimGetRequest<TKey> : IFilterExpressionDapper
        where TKey : IEquatable<TKey>
    {
        //TODO: Modifiar Parameter para que sea dinamico a partir de TKey
        [Parameter("@UserId", type: DbType.Int32, Order = 1, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }

        [Parameter("@ClaimType", type: DbType.String, Order = 2, Direction = ParameterDirection.Input)]
        public string ClaimType { get; set; }

        [Parameter("@ClaimValue", type: DbType.String, Order = 3, Direction = ParameterDirection.Input)]
        public string ClaimValue { get; set; }

    }
}
