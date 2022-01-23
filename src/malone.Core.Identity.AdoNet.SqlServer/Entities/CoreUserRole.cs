using malone.Core.AdoNet.Attributes;
using System;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities
{
    public class CoreUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        [DbParameter("@UserId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        [DbParameter("@RoleId", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public virtual TKey RoleId { get; set; }

    }

    public class CoreUserRole : CoreUserRole<int>
    {
        public CoreUserRole() : base() { }

    }
}
