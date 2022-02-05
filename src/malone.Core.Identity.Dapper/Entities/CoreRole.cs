using System;
using System.Collections.Generic;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("Roles")]
    public class CoreRole<TKey, TUserRole> : IRole<TKey>, IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {
        public CoreRole()
        {
            Users = new List<TUserRole>();
        }

        [Column("Id", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public TKey Id { get; set; }

        [Column("Name", type: DbType.String, Size = 256, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

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
