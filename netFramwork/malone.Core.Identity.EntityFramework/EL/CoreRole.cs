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


    public class CoreRole: CoreRole<int, CoreUserRole>, IBaseEntity
    {
        public CoreRole()
        {
            Id = this.GetHashCode();
        }
    }
}
