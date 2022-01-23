using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace malone.Core.Identity.EntityFramework.Entities
{
    public class CoreUserClaim<TKey> : IdentityUserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public CoreUserClaim() : base() { }

    }

    public class CoreUserClaim : CoreUserClaim<int>
    {
        public CoreUserClaim() : base() { }
    }
}
