using System;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.Entities
{
	public class CoreUser<TKey, TUserLogin, TUserRole, TUserClaim> : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, IBaseEntity<TKey>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>
		where TUserRole : CoreUserRole<TKey>
		where TUserClaim : CoreUserClaim<TKey>
	{
	}

	//public class CoreUser<TKey> : CoreUser<TKey, CoreUserLogin<TKey>, CoreUserRole<TKey>, CoreUserClaim<TKey>>, IBaseEntity<TKey>
	//    where TKey : IEquatable<TKey>
	//{ }

	public class CoreUser : CoreUser<int, CoreUserLogin, CoreUserRole, CoreUserClaim>, IBaseEntity
	{
		public CoreUser()
		{
			Id = this.GetHashCode();
		}

	}
}
