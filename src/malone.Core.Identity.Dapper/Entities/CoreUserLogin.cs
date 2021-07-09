using malone.Core.Dapper.Attributes;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("UsersLogins")]
    public class CoreUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     The login provider for the login (i.e. facebook, google)
        /// </summary>
        [Column("LoginProvider", type: DbType.String, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        ///     Key representing the login for the provider
        /// </summary>
        [Column("ProviderKey", type: DbType.String, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string ProviderKey { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        [Column("UserId", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }
    }

    public class CoreUserLogin : CoreUserLogin<int>
    {

    }
}
