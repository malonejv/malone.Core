using System;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.EF.Entities.Filters;
using malone.Core.Logging;
using malone.Core.Sample.EF.SqlServer.Middle.CL.Exceptions;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Services;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations
{
    public class TodoListBC : Service<TodoList>, ITodoListBC
    {
        protected IErrorLocalizationHandler ErrorLocalizationHandler { get; }

        public TodoListBC(IRepository<TodoList> repository,IUnitOfWork uow, ICoreLogger logger, IErrorLocalizationHandler errorLocalizationHandler)
            : base(logger, uow,repository)
        {
            ErrorLocalizationHandler = errorLocalizationHandler;
        }

        protected override ValidationResultList BeforeAddingValidation(TodoList entity)
        {
            var validationList = base.BeforeAddingValidation(entity);

            validationList.Add(ValidarCaracteresEspeciales(entity));
            validationList.Add(ValidarNombreRepetido(entity));

            return validationList;
        }

        protected override ValidationResultList BeforeUpdatingValidation(TodoList entity)
        {
            var validationList = base.BeforeUpdatingValidation(entity);

            validationList.Add(ValidarCaracteresEspeciales(entity));
            validationList.Add(ValidarNombreRepetido(entity));

            return validationList;
        }

        private ValidationResult ValidarCaracteresEspeciales(TodoList entity)
        {
            try
            {
                bool tieneCaracteresEspeciales = entity.Name.HasSpecialCharacters();

                if (tieneCaracteresEspeciales)
                {
                    string message = ErrorLocalizationHandler.GetString(ErrorCode.BUSVAL5000);
                    return new ValidationResult(ErrorCode.BUSVAL5000.ToString(), message);
                }

                return new ValidationResult();
            }
            catch (Exception ex)
            {
                var techEx = ExceptionFactory<ErrorCode, IErrorLocalizationHandler>.CreateException<TechnicalException>(ex, ErrorCode.TECH1000);
                if (logger != null) logger.Error(techEx);

                throw techEx;
            }
        }

        private ValidationResult ValidarNombreRepetido(TodoList entity)
        {
            bool existe = false;

            existe = repository.Get(
                new FilterExpression<TodoList>()
                {
                    Expression = f => f.Name == entity.Name &&
                                  f.IsDeleted == false &&
                                  f.User.Id == entity.User.Id &&
                                  f.Id != entity.Id
                }).Any();

            if (existe)
            {
                var message = ErrorLocalizationHandler.GetString(ErrorCode.BUSVAL5001);
                return new ValidationResult(ErrorCode.BUSVAL5001.ToString(), message);
            }

            return new ValidationResult();
        }

    }
}
