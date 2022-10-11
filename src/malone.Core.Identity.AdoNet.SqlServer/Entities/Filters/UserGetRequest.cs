using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class UserGetRequest : IFilterExpressionAdoNet
    {
        [Column("@UserNameOrEmail", Type = DbType.String, Order = 1, Direction = ParameterDirection.Input)]
        public string UserNameOrEmail { get; set; }

        //[Column("@Email", Type = DbType.String, Order = 2, Direction = ParameterDirection.Input)]
        //public string Email { get; set; }
    }
}
