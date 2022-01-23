using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace malone.Core.Identity.EntityFramework.Entities
{
    public class CoreUserRole<TKey> : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public CoreUserRole() : base() { }

    }

    public class CoreUserRole : CoreUserRole<int>
    {
        public CoreUserRole() : base() { }

    }
}
