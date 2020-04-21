using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.EL
{
    public class CoreUserLogin<TKey> : IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        public CoreUserLogin() : base() { }

    }

    public class CoreUserLogin : CoreUserLogin<int>
    {
        public CoreUserLogin() : base() { }

    }
}
