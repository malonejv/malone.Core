using malone.Core.AdoNet.DAL.Attributes;
using malone.Core.AdoNet.EL.Filters;
using System.Data;

namespace malone.Core.Sample.Middle.EL.Filters.AdoNet.TodoListEntity
{
    public class ANTaskItemGetRequest : IFilterExpressionAdoNet
    {
        [DbParameter(BindOnNull = false, Direction = ParameterDirection.Input, Size = 100, Type = SqlDbType.NVarChar)]
        public string Description { get; set; }
    }
}
