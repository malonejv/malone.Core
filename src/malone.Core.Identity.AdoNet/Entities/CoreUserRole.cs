using System;

namespace malone.Core.Identity.AdoNet.Entities
{
    public class CoreUserRole<TKey> 
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        public virtual TKey RoleId { get; set; }

    }

    public class CoreUserRole : CoreUserRole<int>
    {
        public CoreUserRole():base() { }

    }
}
