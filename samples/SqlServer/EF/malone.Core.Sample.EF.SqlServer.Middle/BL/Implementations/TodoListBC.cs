using malone.Core.Services;
using malone.Core.Logging;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations
{
    public class TodoListBC : Service<TodoList, ITodoListBV>, ITodoListBC
    {
        public TodoListBC(ITodoListBV businessValidator, IRepository<TodoList> repository, ICoreLogger logger)
            : base(businessValidator, repository, logger)
        { }

        public override void Add(TodoList entity, bool saveChanges = true, bool disposeUoW = true)
        {
            if (!entity.Date.HasValue) entity.Date = DateTime.Now.Date;
            entity.IsDeleted = false;

            ServiceValidator.AddValidationRules
                .AddRange(new List<ValidationRule> {
                        new ValidationRule()
                        {
                            Method = ServiceValidator.ValidarCaracteresEspeciales,
                            Arguments = new List<object>() { entity }
                        },
                        new ValidationRule()
                        {
                            Method = ServiceValidator.ValidarNombreRepetido,
                            Arguments = new List<object>() { entity }
                        }
                });

            //var user = entity.User;
            //entity.User = null;
            base.Add(entity, disposeUoW: false);

            //entity.User = user;
            //base.Update(entity.Id, entity);
        }

    }
}
