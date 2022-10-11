using System.Security.Claims;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.Entities
{
	public static class CoreUserExtensions
	{
		public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this CoreUser user, UserService manager, string authenticationType)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);
			// Add custom user claims here
			return userIdentity;
		}
	}
}
