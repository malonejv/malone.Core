using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager;
using malone.Core.DAL.Repositories;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL.Filters.EF.TodoListEntity;
using malone.Core.Sample.Middle.EL.Model;
using System;
using System.Linq;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TodoListBV : BusinessValidator<TodoList, ErrorCodes>, ITodoListBV
    {
        protected ICoreRepository<TodoList, ErrorCodes> Repository { get; }

        public TodoListBV(ICoreRepository<TodoList, ErrorCodes> repository, IMessageHandler<ErrorCodes> messageHandler, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base(messageHandler, exceptionHandler)
        {
            Repository = repository;
        }

        public ValidationResult ValidarNombreRepetido(params object[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException("args");

            var todoList = args[0] as TodoList;

            bool existe = false;

            existe = Repository.Get(new EFTodoListGetRequest()
            {
                Expression = f => f.Name == todoList.Name && f.Id != todoList.Id
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
