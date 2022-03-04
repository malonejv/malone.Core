using System;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class UserClaimGetRequest<TKey> : IFilterExpressionAdoNet
        where TKey : IEquatable<TKey>
    {
        //TODO: Modifiar Column para que sea dinamico a partir de TKey
        [Column("@UserId", Type = DbType.Int32, Order = 1, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }

        [Column("@ClaimType", Type = DbType.String, Order = 2, Direction = ParameterDirection.Input)]
        public string ClaimType { get; set; }

        [Column("@ClaimValue", Type = DbType.String, Order = 3, Direction = ParameterDirection.Input)]
        public string ClaimValue { get; set; }

    }
}
