using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TodoListBC : BusinessComponent<TodoList, ITodoListBV, ErrorCodes>, ITodoListBC
    {
        public TodoListBC(IUnitOfWork unitOfWork, ITodoListBV businessValidator, ICoreRepository<TodoList, ErrorCodes> repository, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
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
