using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.Entities
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
