using System;
using System.Collections.Generic;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreRole<TKey, TUserRole> : IRole<TKey>, IBaseEntity<TKey>
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
        ///     Role id
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; private set; }

    }


    public class CoreRole<TUserRole> : CoreRole<int, TUserRole>, IBaseEntity
        where TUserRole : CoreUserRole
    {
        public CoreRole()
        {
            Id = this.GetHashCode();
        }
    }

    public class CoreRole : CoreRole<CoreUserRole>
    { }
}
