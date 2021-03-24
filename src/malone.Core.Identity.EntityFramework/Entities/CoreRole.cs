using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace malone.Core.Identity.EntityFramework.Entities
{
    public class CoreRole<TKey, TUserRole> : IdentityRole<TKey, TUserRole>, IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {

    }


    public class CoreRole : CoreRole<int, CoreUserRole>, IBaseEntity
    {
        public CoreRole()
        {
            Id = this.GetHashCode();
        }
    }
}
