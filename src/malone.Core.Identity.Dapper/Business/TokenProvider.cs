﻿using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace malone.Core.Identity.Dapper.Business
{

    public static class TokenProvider<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        private static DataProtectorTokenProvider<TUserEntity, TKey> _tokenProvider;

        public static DataProtectorTokenProvider<TUserEntity, TKey> Provider
        {
            get
            {

                if (_tokenProvider == null)
                {
                    var provider = new DpapiDataProtectionProvider();
                    var entropy = "D4151DA419C4691E";
                    _tokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(provider.Create(entropy))
                    {
                        TokenLifespan = TimeSpan.FromDays(1)
                    };
                }

                return _tokenProvider;
            }
        }
    }
}