using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.EL
{
    public class CoreUserRole<TKey> : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public CoreUserRole() : base() { }

    }

    public class CoreUserRole : CoreUserRole<int>
    {
        public CoreUserRole():base() { }

    }
}
