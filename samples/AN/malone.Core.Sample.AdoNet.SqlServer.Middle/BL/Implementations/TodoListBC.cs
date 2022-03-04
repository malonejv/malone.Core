using System;
using System.Collections.Generic;
using malone.Core.Logging;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Services;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations
{
	public class TodoListBC : Service<TodoList, ITodoListBV>, ITodoListBC
    {
        public TodoListBC(ITodoListBV businessValidator, IQueryService<int, TodoList,ITodoListBV> queryService, IDataManipulationService<int, TodoList, ITodoListBV> dataManipulationService, ICoreLogger logger)
            : base(businessValidator, queryService, dataManipulationService, logger)
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
