using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class RoleGetRequest : IFilterExpressionAdoNet
    {
        [Column("@Name", Type = DbType.String, Order = 1, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

    }
}
