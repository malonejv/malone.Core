﻿using malone.Core.Identity.Dapper.Business;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace malone.Core.Identity.Dapper.Entities
{
    public static class CoreUserExtensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>(this TUserEntity user, UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> manager, string authenticationType)
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}