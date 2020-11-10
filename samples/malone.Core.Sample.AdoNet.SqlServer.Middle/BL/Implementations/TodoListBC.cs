using malone.Core.Business.Components;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations
{
    public class TodoListBC : BusinessComponent<TodoList, ITodoListBV>, ITodoListBC
    {
        public TodoListBC(ITodoListBV businessValidator, IRepository<TodoList> repository, ILogger logger)
            : base(businessValidator, repository, logger)
        { }

        public override void Add(TodoList entity, bool saveChanges = true, bool disposeUoW = true)
        {
            if (!entity.Date.HasValue) entity.Date = DateTime.Now.Date;
            entity.IsDeleted = false;

            BusinessValidator.AddValidationRules
                .AddRange(new List<ValidationRule> {
                        new ValidationRule()
                        {
                            Method = BusinessValidator.ValidarCaracteresEspeciales,
                            Arguments = new List<object>() { entity }
                        },
                        new ValidationRule()
                        {
                            Method = BusinessValidator.ValidarNombreRepetido,
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
