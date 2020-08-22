using System;

namespace malone.Core.Identity.AdoNet.Entities
{
    public class CoreUserClaim<TKey> 
        where TKey : IEquatable<TKey>
    {
        public CoreUserClaim() { }

        /// <summary>
        ///     Primary key
        /// </summary>
        public virtual int Id { get; set; }

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

    public class CoreUserClaim : CoreUserClaim<int>
    {
        public CoreUserClaim():base() { }
    }
}
