using malone.Core.AdoNet.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Logging;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AN.SqlServer.Middle.DAL.Repositories
{
    public class TodoListRepository : Repository<TodoList>
    {
        public TodoListRepository(IContext context, ICoreLogger logger) : base(context, logger)
        {
        }
    }
}
