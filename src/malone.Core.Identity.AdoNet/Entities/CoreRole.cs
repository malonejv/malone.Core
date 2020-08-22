using System;
using System.Collections.Generic;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.Entities
{
    public class CoreRole<TKey, TUserRole> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public CoreRole()
        {
            Users = new List<TUserRole>();
        }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; private set; }

        /// <summary>
        ///     Role id
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        public string Name { get; set; }
    }


    public class CoreRole: CoreRole<int, CoreUserRole>, IBaseEntity
    {
        public CoreRole()
        {
            Id = this.GetHashCode();
        }
    }
}
