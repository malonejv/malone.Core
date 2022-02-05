using System;
using System.Collections.Generic;
using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("Users")]
    public class CoreUser<TKey, TUserLogin, TUserRole, TUserClaim> : IUser<TKey>, IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
    where TUserLogin : CoreUserLogin<TKey>
    where TUserRole : CoreUserRole<TKey>
    where TUserClaim : CoreUserClaim<TKey>
    {
        public CoreUser()
        {
            Logins = null;
            Roles = null;
            Claims = null;
        }

        [Column("Email", type: DbType.String, Size = 256, Direction = ParameterDirection.Input)]
        public virtual string Email { get; set; }

        [Column("EmailConfirmed", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool EmailConfirmed { get; set; }

        [Column("PasswordHash", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PasswordHash { get; set; }

        [Column("SecurityStamp", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string SecurityStamp { get; set; }

        [Column("PhoneNumber", type: DbType.String, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PhoneNumber { get; set; }

        [Column("PhoneNumberConfirmed", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [Column("TwoFactorEnabled", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool TwoFactorEnabled { get; set; }

        [Column("LockoutEndDateUtc", type: DbType.DateTime, Direction = ParameterDirection.Input)]
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        [Column("LockoutEnabled", type: DbType.Boolean, Direction = ParameterDirection.Input)]
        public virtual bool LockoutEnabled { get; set; }

        [Column("AccessFailedCount", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual int AccessFailedCount { get; set; }

        public virtual ICollection<TUserRole> Roles { get; internal set; }

        public virtual ICollection<TUserClaim> Claims { get; internal set; }

        public virtual ICollection<TUserLogin> Logins { get; internal set; }

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
