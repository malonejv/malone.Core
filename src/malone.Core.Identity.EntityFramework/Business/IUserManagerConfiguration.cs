﻿using malone.Core.Identity;
using malone.Core.Identity.EntityFramework.Entities;
using System;

namespace malone.Core.Identity.EntityFramework.Business
{
    public interface IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TUserBC : UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
    {
        TUserBC UserBC { get; set; }
        IEmailMessageService EmailService { get; set; }
        ISmsMessageService SmsService { get; set; }

        void ConfigureUserManager();
    }

    public interface IUserManagerConfiguration : IUserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>
    {
    }
}