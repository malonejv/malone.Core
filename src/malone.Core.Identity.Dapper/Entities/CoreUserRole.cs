using malone.Core.Dapper.Attributes;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities
{
    [Table("UsersRoles")]
    public class CoreUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        [Column("UserId", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        [Column("RoleId", type: DbType.Int32, Direction = ParameterDirection.Input)]
        public virtual TKey RoleId { get; set; }

    }

    public class CoreUserRole : CoreUserRole<int>
    {
        public CoreUserRole() : base() { }

    }
}
