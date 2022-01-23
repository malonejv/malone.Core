using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Filters
{
    public class TodoListGetRequest : IFilterExpressionAdoNet
    {
        [StringLength(100)]
        [DbParameter("@Name", Type = FbDbType.VarChar, Order = 1, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        [DbParameter("@IsDeleted", Type = FbDbType.SmallInt, Order = 2, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [DbParameter("@UserId", Type = FbDbType.Integer, Order = 3, Direction = ParameterDirection.Input)]
        public int UserId { get; set; }

        [DbParameter("@Id", Type = FbDbType.Integer, Order = 4, Direction = ParameterDirection.Input)]
        public int Id { get; set; }
    }
}
