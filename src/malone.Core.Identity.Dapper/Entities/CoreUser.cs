using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("Users")]
    public class CoreUser<TKey, TUserLogin, TUserRole, TUserClaim> : IUser<TKey>, IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
    where TUserLogin : CoreUserLogin<TKey>
    where TUserRole : CoreUserRole<TKey>
    where TUserClaim : CoreUserClaim<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public CoreUser()
        {
            Logins = null;
            Roles = null;
            Claims = null;
        }

        /// <summary>
        ///     Email
        /// </summary>
        [Column("Email", type: DbType.String, Size = 256, Direction = ParameterDirection.Input)]
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        [Column("EmailConfirmed", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        [Column("PasswordHash", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        [Column("SecurityStamp", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        [Column("PhoneNumber", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        [Column("PhoneNumberConfirmed", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        [Column("TwoFactorEnabled", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        [Column("LockoutEndDateUtc", type: DbType.DateTime, Direction = ParameterDirection.Input)]
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        [Column("LockoutEnabled", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        [Column("AccessFailedCount", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<TUserRole> Roles { get; internal set; }

        /// <summary>
        ///     Navigation property for user claims
        /// </summary>
        public virtual ICollection<TUserClaim> Claims { get; internal set; }

        /// <summary>
        ///     Navigation property for user logins
        /// </summary>
        public virtual ICollection<TUserLogin> Logins { get; internal set; }

        /// <summary>
        ///     User ID (Primary Key)
        /// </summary>
        [Column("Id", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        [Column("UserName", type: DbType.String, Size = 256, Direction = ParameterDirection.Input)]
        public string UserName { get; set; }
    }


    public class CoreUser<TUserLogin, TUserRole, TUserClaim> : CoreUser<int, TUserLogin, TUserRole, TUserClaim>, IBaseEntity
        where TUserLogin : CoreUserLogin
        where TUserRole : CoreUserRole
        where TUserClaim : CoreUserClaim
    {
        public CoreUser()
        {
            Id = this.GetHashCode();
        }
    }

    public class CoreUser : CoreUser<CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
    }
}
