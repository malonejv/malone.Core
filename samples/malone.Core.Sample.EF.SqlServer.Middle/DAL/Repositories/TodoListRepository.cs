using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Repositories
{
    public class TodoListRepository : EFRepository<TodoList>
    {
        public TodoListRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        public override void Insert(TodoList entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Entry(entity.User).State = EntityState.Unchanged;

            base.Insert(entity);
        }
    }
}
