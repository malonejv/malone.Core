﻿using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("Roles")]
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
        [Column("Id", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public TKey Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        [Column("Name", type: DbType.String, Size = 256, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; internal set; }

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
