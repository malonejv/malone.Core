using System;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserClaim<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [Column("@Id", Type = DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        [Column("@UserId", Type = DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        [Column("@ClaimType", Type = DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimType { get; set; }

        [Column("@ClaimValue", Type = DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimValue { get; set; }

    }

    public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
    {
        public CoreUserClaim() : base() { }
    }
}
