using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.Entities
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
