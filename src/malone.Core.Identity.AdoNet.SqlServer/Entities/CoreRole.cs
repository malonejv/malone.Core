using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreRole<TKey, TUserRole> : IRole<TKey>, IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {
                                public CoreRole()
        {
            Users = new List<TUserRole>();
        }

                                [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public TKey Id { get; set; }

                                [DbParameter("@Name", Type = SqlDbType.NVarChar, Size = 256, Direction = ParameterDirection.Input)]
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
