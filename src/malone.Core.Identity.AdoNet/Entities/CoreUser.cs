﻿using System;
using System.Collections.Generic;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.Entities
{
    public class CoreUser<TKey, TUserLogin, TUserRole, TUserClaim> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserClaim : CoreUserClaim<TKey>
    {

        public CoreUser()
        {
            Claims = new List<TUserClaim>();
            Roles = new List<TUserRole>();
            Logins = new List<TUserLogin>();
        }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<TUserRole> Roles { get; private set; }

        /// <summary>
        ///     Navigation property for user claims
        /// </summary>
        public virtual ICollection<TUserClaim> Claims { get; private set; }

        /// <summary>
        ///     Navigation property for user logins
        /// </summary>
        public virtual ICollection<TUserLogin> Logins { get; private set; }

        /// <summary>
        ///     User ID (Primary Key)
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     User name
        /// </summary>
        public virtual string UserName { get; set; }
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

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public CoreUser(string userName)
            : this()
        {
            UserName = userName;
        }
    }
}
