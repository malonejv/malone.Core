using malone.Core.DAL.AdoNet.Attributes;
using malone.Core.EL.Filters;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace malone.Core.Sample.Middle.EL.Filters.AdoNetFilters.TodoList
{
    public class ANTodoListGetRequest : IFilterExpressionAdoNet
    {
        [DbParameter(BindOnNull = false, Direction = ParameterDirection.Input, Size = 100, Type = SqlDbType.NVarChar)]
        public string Name { get; set; }

    }
}
