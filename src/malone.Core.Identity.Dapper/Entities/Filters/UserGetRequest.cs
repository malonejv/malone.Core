using System.Data;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class UserGetRequest : IFilterExpressionDapper
    {
        [Parameter("@UserNameOrEmail", type: DbType.String, Order = 1, Direction = ParameterDirection.Input)]
        public string UserNameOrEmail { get; set; }

        //[Parameter("@Email", type: DbType.String, Order = 2, Direction =  ParameterDirection.Input)]
        //public string Email { get; set; }
    }
}
