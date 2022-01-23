using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("UsersClaims")]
    public class CoreUserClaim<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        [Column("Id", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        [Column("UserId", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        [Column("ClaimType", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        [Column("ClaimValue", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimValue { get; set; }

    }

    public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
    {
        public CoreUserClaim() : base() { }
    }
}
