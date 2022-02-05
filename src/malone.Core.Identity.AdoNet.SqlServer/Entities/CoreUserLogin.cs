using System;
using System.Data;
using malone.Core.AdoNet.Attributes;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        [DbParameter("@LoginProvider", Type = SqlDbType.NVarChar, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string LoginProvider { get; set; }

        [DbParameter("@ProviderKey", Type = SqlDbType.NVarChar, Size = 128, Direction = ParameterDirection.Input)]
        public virtual string ProviderKey { get; set; }

        [DbParameter("@UserId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }
    }

    public class CoreUserLogin : CoreUserLogin<int>
    {

    }
}
