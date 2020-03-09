﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.DAL.Base.Repositories;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Filters;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TodoListBV : BusinessValidator<TodoList>, ITodoListBV
    {
        protected IRepository<TodoList> Repository { get; }

        public TodoListBV(IRepository<TodoList> repository, IExceptionMessageManager exManager, IExceptionHandler exHandler)
            : base(exManager, exHandler)
        {
            Repository = repository;
        }

        public ValidationFailure ValidarNombreRepetido(params object[] args)
        {

            if (args == null || args.Length == 0)
                throw new ArgumentNullException("args");

            var todoList = args[0] as TodoList;

            var existe = Repository.Get(new TodoListFilter()
            {
                Expression = f => f.Name == todoList.Name && f.Id != todoList.Id
            }).Count() > 0;

            if (existe)
            {
                var message = string.Format(MessageManager.GetDescription((int)ErrorCodes.E1300), typeof(TodoList));
                return  new ValidationFailure((int)ErrorCodes.E1300, message);
            }

            return null;
        }
    }
}
