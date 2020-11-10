using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters
{
    public class TodoListGetRequest : IFilterExpressionAdoNet
    {
        [StringLength(100)]
        [DbParameter("@Name", Type = SqlDbType.NVarChar, Order = 1, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        [DbParameter("@IsDeleted", Type = SqlDbType.Bit, Order = 2, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [DbParameter("@UserId", Type = SqlDbType.Int, Order = 3, Direction = ParameterDirection.Input)]
        public int UserId { get; set; }

        [DbParameter("@Id", Type = SqlDbType.Int, Order = 4, Direction = ParameterDirection.Input)]
        public int Id { get; set; }
    }
}
