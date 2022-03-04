using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Dapper.Entities.Filters;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters
{
	public class TodoListGetRequest : IFilterExpressionDapper
    {
        [StringLength(100)]
        [Parameter("@Name", DbType.String, Order = 1, Direction = ParameterDirection.Input, IgnoreDbNull = true)]
        public string Name { get; set; }

        [Parameter("@IsDeleted", DbType.Boolean, Order = 2, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [Parameter("@UserId", DbType.Int32, Order = 3, Direction = ParameterDirection.Input, IgnoreDbNull = true)]
        public int? UserId { get; set; }

        [Parameter("@Id", DbType.Int32, Order = 4, Direction = ParameterDirection.Input, IgnoreDbNull = true)]
        public int? Id { get; set; }
    }
}
