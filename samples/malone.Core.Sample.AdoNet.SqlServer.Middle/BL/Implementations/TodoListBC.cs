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
            try
            {
                BusinessValidator.AddValidationRules
                    .Add(
                        new ValidationRule()
                        {
                            Method = BusinessValidator.ValidarNombreRepetido,
                            Arguments = new List<object>() { entity }
                        });

                base.Add(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
