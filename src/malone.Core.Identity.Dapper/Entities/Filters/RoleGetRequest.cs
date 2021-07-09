using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;
using System.Data;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class RoleGetRequest : IFilterExpressionDapper
    {
        [Parameter(name: "@Name", type: DbType.String, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

    }
}
