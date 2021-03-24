using malone.Core.AdoNet.Attributes;
using System;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     The login provider for the login (i.e. facebook, google)
        /// </summary>
        [DbParameter("@LoginProvider", Type = SqlDbType.NVarChar, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        ///     Key representing the login for the provider
        /// </summary>
        [DbParameter("@ProviderKey", Type = SqlDbType.NVarChar, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string ProviderKey { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        [DbParameter("@UserId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }
    }

    public class CoreUserLogin : CoreUserLogin<int>
    {

    }
}
