using malone.Core.EF.Entities.Filters;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System;
using System.Linq.Expressions;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.Filters.EF.TodoListEntity
{
    public class EFTodoListGetRequest: IFilterExpressionEF<TodoList>
    {
        public Expression<Func<TodoList, bool>> Expression { get; set; }
    }
}
