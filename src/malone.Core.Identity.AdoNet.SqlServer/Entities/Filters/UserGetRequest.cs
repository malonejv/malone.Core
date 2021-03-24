using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class UserGetRequest : IFilterExpressionAdoNet
    {
        [DbParameter("@UserNameOrEmail", Type = SqlDbType.NVarChar, Order = 1, Direction = ParameterDirection.Input)]
        public string UserNameOrEmail { get; set; }

        //[DbParameter("@Email", Type = SqlDbType.NVarChar, Order = 2, Direction = ParameterDirection.Input)]
        //public string Email { get; set; }
    }
}
