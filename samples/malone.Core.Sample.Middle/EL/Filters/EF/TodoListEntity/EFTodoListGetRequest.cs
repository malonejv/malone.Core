using malone.Core.EF.EL.Filters;
using malone.Core.Sample.Middle.EL.Model;
using System;
using System.Linq.Expressions;

namespace malone.Core.Sample.Middle.EL.Filters.EF.TodoListEntity
{
    public class EFTodoListGetRequest: IFilterExpressionEF<TodoList>
    {
        public Expression<Func<TodoList, bool>> Expression { get; set; }
    }
}
