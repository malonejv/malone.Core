using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class UserLoginGetRequest<TKey> : IFilterExpressionAdoNet
        where TKey : IEquatable<TKey>
    {
        [StringLength(100)]
        [DbParameter("@LoginProvider", Type = SqlDbType.NVarChar, Order = 1, Direction = ParameterDirection.Input)]
        public string LoginProvider { get; set; }

        [StringLength(100)]
        [DbParameter("@ProviderKey", Type = SqlDbType.NVarChar, Order = 2, Direction = ParameterDirection.Input)]
        public string ProviderKey { get; set; }

        //TODO: Esto esta condicionando a usar int
        [DbParameter("@UserId", Type = SqlDbType.Int, Order = 3, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }
    }
}
