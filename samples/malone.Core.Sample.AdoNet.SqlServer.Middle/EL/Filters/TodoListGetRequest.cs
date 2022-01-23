using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters
{
    public class TodoListGetRequest : IFilterExpressionAdoNet
    {
        [StringLength(100)]
        [DbParameter("@Name", Type = SqlDbType.NVarChar, Order = 2, Direction = ParameterDirection.Input)]
        public string Name { get; set; }
    }
}
