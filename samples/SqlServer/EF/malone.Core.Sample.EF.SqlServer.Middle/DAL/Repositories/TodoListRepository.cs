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

        public override void Add(TodoList entity)
        {
            //Context.Entry(entity).State = EntityState.Added;
            //if (entity.User != null) Context.Entry(entity.User).State = EntityState.Unchanged;
            //if (entity.User.Roles.Count > 0)
            //    foreach (var role in entity.User.Roles) Context.Entry(role).State = EntityState.Unchanged;

            base.Add(entity);
        }

        public override void Update(TodoList entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            //if (entity.Items != null) Update(entity.Items);
            if (entity.User != null) Context.Entry(entity.User).State = EntityState.Unchanged;
        }
    }
}
