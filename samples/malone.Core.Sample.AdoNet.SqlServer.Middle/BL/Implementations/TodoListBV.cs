using System;
using System.Linq;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.AdoNet.SqlServer.Middle.CL.Exceptions;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations
{
    public class TodoListBV : BusinessValidator<TodoList>, ITodoListBV
    {
        protected IRepository<TodoList> Repository { get; }

        protected IErrorLocalizationHandler ErrorLocalizationHandler { get; }
        protected ILogger Logger { get; }

        public TodoListBV(IRepository<TodoList> repository, IErrorLocalizationHandler errorLocalizationHandler, ILogger logger)
            : base()
        {
            Repository = repository;
            ErrorLocalizationHandler = errorLocalizationHandler;
            Logger = logger;
        }

        /// <summary>
        /// Valida que el texto de la entidad Ejemplo no tenga caracteres especiales
        /// </summary>
        /// <param name="args">Recibe una instancia de la clase Ejemplo</param>
        /// <returns></returns>
        public ValidationResult ValidarCaracteresEspeciales(params object[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    throw new ArgumentNullException("args");

                var todoList = args[0] as TodoList;

                bool tieneCaracteresEspeciales = todoList.Name.HasSpecialCharacters();

                if (tieneCaracteresEspeciales)
                {
                    var message = ErrorLocalizationHandler.GetString(ErrorCode.BUSVAL5000);
                    //var formated = string.Format(message, typeof(Ejemplo));
                    return new ValidationResult(ErrorCode.BUSVAL5000.ToString(), message);
                }

                return null;
            }
            catch (Exception ex)
            {
                var techEx = ExceptionFactory<ErrorCode, IErrorLocalizationHandler>.CreateException<TechnicalException>(ex, ErrorCode.TECH1000);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        public ValidationResult ValidarNombreRepetido(params object[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException("args");

            var todoList = args[0] as TodoList;

            bool existe = false;

            existe = Repository.Get(new TodoListGetRequest()
            {
                Name = todoList.Name,
                IsDeleted = false,
                UserId = todoList.User.Id,
                Id = todoList.Id
            }).Any();

            if (existe)
            {
                var message = ErrorLocalizationHandler.GetString(ErrorCode.BUSVAL5001);
                return new ValidationResult(ErrorCode.BUSVAL5001.ToString(), message);
            }

            return null;
        }

    }
}
