using System;
using System.Security.Claims;
using System.Threading.Tasks;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.Entities
{
	public class CoreUser<TKey, TUserLogin, TUserRole, TUserClaim> : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, IBaseEntity<TKey>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>
		where TUserRole : CoreUserRole<TKey>
		where TUserClaim : CoreUserClaim<TKey>
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CoreUser<TKey,TUserLogin,TUserRole,TUserClaim>,TKey> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
			// Add custom user claims here
			return userIdentity;
		}
	}
	
	public class CoreUser : CoreUser<int, CoreUserLogin, CoreUserRole, CoreUserClaim>, IBaseEntity
	{
		public CoreUser()
		{
			Id = this.GetHashCode();
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CoreUser, int> manager,string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
			// Add custom user claims here
			return userIdentity;
		}

	}
}
