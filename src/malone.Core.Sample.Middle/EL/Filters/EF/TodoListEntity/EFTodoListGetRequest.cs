using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EF.EL.Filters;
using malone.Core.EL.Filters;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.EL.Filters.EF.TodoListEntity
{
    public class EFTodoListGetRequest: IFilterExpressionEF<TodoList>
    {
        public Expression<Func<TodoList, bool>> Expression { get; set; }
    }
}
