using malone.Core.Commons.Helpers.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace malone.Core.Identity
{
    public static class AuthenticationManagerExtensions
    {

        /// <summary>
        ///     Returns true if there is a TwoFactorRememberBrowser cookie for a user
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static async Task<bool> TwoFactorBrowserRememberedAsync<TKey>(this IAuthenticationManager manager,
            TKey userId)
            where TKey : IEquatable<TKey>,IConvertible
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            var result =
                await manager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie).WithCurrentCulture();
            return (result != null && result.Identity != null && result.Identity.GetUserId<TKey>().Equals(userId));
        }

        /// <summary>
        ///     Returns true if there is a TwoFactorRememberBrowser cookie for a user
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Creates a TwoFactorRememberBrowser cookie for a user
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
