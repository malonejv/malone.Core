using malone.Core.EF.Entities.Filters;
using GMS.Core.EL.Model;
using System;
using System.Linq.Expressions;

namespace GMS.Core.EL.Filters.EF.TodoListEntity
{
    public class EFTodoListGetRequest: IFilterExpressionEF<TodoList>
    {
        public Expression<Func<TodoList, bool>> Expression { get; set; }
    }
}
