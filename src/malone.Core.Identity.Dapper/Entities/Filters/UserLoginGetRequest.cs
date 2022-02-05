using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class UserLoginGetRequest<TKey> : IFilterExpressionDapper
        where TKey : IEquatable<TKey>
    {
        [StringLength(100)]
        [Parameter("@LoginProvider", type: DbType.String, Order = 1, Direction = ParameterDirection.Input)]
        public string LoginProvider { get; set; }

        [StringLength(100)]
        [Parameter("@ProviderKey", type: DbType.String, Order = 2, Direction = ParameterDirection.Input)]
        public string ProviderKey { get; set; }

        //TODO: Esto esta condicionando a usar int
        [Parameter("@UserId", type: DbType.Int32, Order = 3, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }
    }
}
