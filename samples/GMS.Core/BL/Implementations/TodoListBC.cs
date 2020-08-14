using malone.Core.Business.Components;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using GMS.Core.EL.Model;
using System;
using System.Collections.Generic;

namespace GMS.Core.BL.Implementations
{
    public class TodoListBC : BusinessComponent<TodoList, ITodoListBV>, ITodoListBC
    {
        public TodoListBC(IUnitOfWork unitOfWork, ITodoListBV businessValidator, IRepository<TodoList> repository, ILogger logger)
            : base(unitOfWork, businessValidator, repository, logger)
        { }


        public override void Add(TodoList entity)
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
