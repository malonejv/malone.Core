using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.Entities
{
    public static class CoreUserExtensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this CoreUser user,UserBusinessComponent manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
