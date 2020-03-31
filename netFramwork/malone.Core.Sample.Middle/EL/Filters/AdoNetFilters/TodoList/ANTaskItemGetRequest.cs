using malone.Core.DAL.AdoNet.Attributes;
using malone.Core.EL.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.Middle.EL.Filters.AdoNetFilters.TodoList
{
    public class ANTaskItemGetRequest : IFilterExpressionAdoNet
    {
        [DbParameter(BindOnNull = false, Direction = ParameterDirection.Input, Size = 100, Type = SqlDbType.NVarChar)]
        public string Description { get; set; }
    }
}
