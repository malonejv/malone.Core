using System;
using System.Collections.Generic;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
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

        [DbParameter("@Email", Type = SqlDbType.NVarChar, Size = 256, Direction = ParameterDirection.Input)]
        public virtual string Email { get; set; }

        [DbParameter("@EmailConfirmed", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public virtual bool EmailConfirmed { get; set; }

        [DbParameter("@PasswordHash", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PasswordHash { get; set; }

        [DbParameter("@SecurityStamp", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string SecurityStamp { get; set; }

        [DbParameter("@PhoneNumber", Type = SqlDbType.NVarChar, Size = 4000, Direction = ParameterDirection.Input)]
        public virtual string PhoneNumber { get; set; }

        [DbParameter("@PhoneNumberConfirmed", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [DbParameter("@TwoFactorEnabled", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public virtual bool TwoFactorEnabled { get; set; }

        [DbParameter("@LockoutEndDateUtc", Type = SqlDbType.DateTime, Direction = ParameterDirection.Input)]
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        [DbParameter("@LockoutEnabled", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public virtual bool LockoutEnabled { get; set; }

        [DbParameter("@AccessFailedCount", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual int AccessFailedCount { get; set; }

        public virtual ICollection<TUserRole> Roles { get; internal set; }

        public virtual ICollection<TUserClaim> Claims { get; internal set; }

        public virtual ICollection<TUserLogin> Logins { get; internal set; }

        [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey Id { get; set; }

        [DbParameter("@UserName", Type = SqlDbType.NVarChar, Size = 256, Direction = ParameterDirection.Input)]
        public string UserName { get; set; }
    }


    public class CoreUser<TUserLogin, TUserRole, TUserClaim> : CoreUser<int, TUserLogin, TUserRole, TUserClaim>, IBaseEntity
        where TUserLogin : CoreUserLogin
        where TUserRole : CoreUserRole
        where TUserClaim : CoreUserClaim
    {
        public CoreUser() : base()
        {
            Id = this.GetHashCode();
        }
    }

    public class CoreUser : CoreUser<CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public CoreUser() : base()
        { }

    }
}
