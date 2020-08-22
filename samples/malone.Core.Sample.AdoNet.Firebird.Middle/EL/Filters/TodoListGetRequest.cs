using System.ComponentModel.DataAnnotations;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Filters
{
    public class TodoListGetRequest : IFilterExpressionAdoNet
    {
        [StringLength(100)]
        [DbParameter("@Name", Type = FbDbType.VarChar, Order = 2, Direction = ParameterDirection.Input)]
        public string Name { get; set; }
    }
}
