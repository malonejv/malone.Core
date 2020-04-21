using malone.Core.EL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.EL
{
    public class CoreRole<TKey, TUserRole> : IdentityRole<TKey, TUserRole>, IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
    {

    }

    public class CoreRole<TUserRole> : CoreRole<int, TUserRole>, IBaseEntity
        where TUserRole : CoreUserRole<int>
    {
    }

    public class CoreRole: CoreRole<CoreUserRole>, IBaseEntity
    {
        public CoreRole()
        {
            Id = this.GetHashCode();
        }
    }
}
