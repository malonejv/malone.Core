using malone.Core.Logging;
using malone.Core.DataAccess.Context;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System.Data.Entity;

namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Repositories
{
    public class TodoListRepository : Repository<TodoList>
    {
        public TodoListRepository(IContext context, ICoreLogger logger) : base(context, logger)
        {
        }
    }
}
