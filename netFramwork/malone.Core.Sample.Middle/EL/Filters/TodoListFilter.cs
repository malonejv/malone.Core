using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL.Filters;

namespace malone.Core.Sample.Middle.EL.Filters
{
    public class TodoListFilter: IFilterEF<TodoList>
    {
        public Expression<Func<TodoList, bool>> Expression { get; set; }
    }
}
