using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using System;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserClaim<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        [DbParameter("@UserId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        [DbParameter("@ClaimType", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        [DbParameter("@ClaimValue", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string ClaimValue { get; set; }

    }

    public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
    {
        public CoreUserClaim() : base() { }
    }
}
