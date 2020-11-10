using System;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserClaim<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string ClaimValue { get; set; }

    }

    public class CoreUserClaim : CoreUserClaim<int>, IBaseEntity
    {
        public CoreUserClaim() : base() { }
    }
}
