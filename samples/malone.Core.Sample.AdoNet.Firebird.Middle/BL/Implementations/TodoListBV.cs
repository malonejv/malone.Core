using malone.Core.Business.Components;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.AdoNet.Firebird.Middle.CL.Exceptions;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Filters;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using System;
using System.Linq;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.BL.Implementations
{
    public class TodoListBV : BusinessValidator<TodoList>, ITodoListBV
    {
        protected IRepository<TodoList> Repository { get; }

        public TodoListBV(IRepository<TodoList> repository)
            : base()
        {
            Repository = repository;
        }

        public ValidationResult ValidarNombreRepetido(params object[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException("args");

            var todoList = args[0] as TodoList;

            bool existe = false;

            existe = Repository.Get(new TodoListGetRequest()
            {
                Name = todoList.Name
            }).Any();

            if (existe)
            {
                //TODO: Corregir en core
                //ExceptionHandler.HandleException<BusinessValidationException<ErrorCodes>>()
                //var message = string.Format(MessageManager.GetDescription((int)ErrorCodes.E1300), typeof(TodoList));
                //return new ValidationFailure((int)ErrorCodes.E1300, message);
            }

            return null;
        }

    }
}
