using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.Entities
{
    public class CoreUserClaim<TKey> : IdentityUserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public CoreUserClaim() : base() { }

    }

    public class CoreUserClaim : CoreUserClaim<int>
    {
        public CoreUserClaim():base() { }
    }
}
