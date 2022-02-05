using System;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserClaim<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        [DbParameter("@UserId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        [DbParameter("@ClaimType", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimType { get; set; }

        [DbParameter("@ClaimValue", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimValue { get; set; }

    }

    public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
    {
        public CoreUserClaim() : base() { }
    }
}
