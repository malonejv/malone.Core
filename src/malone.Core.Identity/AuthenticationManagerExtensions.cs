using malone.Core.Commons.Helpers.Threading;
using malone.Core.Commons.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace malone.Core.Identity
{
    public static class AuthenticationManagerExtensions
    {

                                                        public static async Task<bool> TwoFactorBrowserRememberedAsync<TKey>(this IAuthenticationManager manager,
            TKey userId)
            where TKey : IEquatable<TKey>, IConvertible
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            var result =
                await manager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie).WithCurrentCulture();
            return (result != null && result.Identity != null && result.Identity.GetUserId<TKey>().Equals(userId));
        }

                                                        public static bool TwoFactorBrowserRemembered<TKey>(this IAuthenticationManager manager,
            TKey userId)
            where TKey : IEquatable<TKey>, IConvertible
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.TwoFactorBrowserRememberedAsync<TKey>(userId));
        }

                                                        public static ClaimsIdentity CreateTwoFactorRememberBrowserIdentity<TKey>(this IAuthenticationManager manager,
            TKey userId)
            where TKey : IEquatable<TKey>, IConvertible
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            var rememberBrowserIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            rememberBrowserIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            return rememberBrowserIdentity;
        }

    }
}
