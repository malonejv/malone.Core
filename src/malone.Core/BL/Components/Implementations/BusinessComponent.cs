using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.DI;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EL.Filters;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.BL.Components.Implementations
{
    public abstract class BusinessComponent<TKey, TEntity, TValidator, TErrorCoder> : IBusinessComponent<TKey, TEntity, TValidator, TErrorCoder>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TValidator : IBusinessValidator<TKey, TEntity, TErrorCoder>
        where TErrorCoder : Enum
    {

        #region Properties

        protected ICoreRepository<TKey, TEntity, TErrorCoder> Repository { get; private set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public TValidator BusinessValidator { get; set; }

        protected IExceptionHandler<TErrorCoder> ExceptionHandler { get; }

        internal ICoreExceptionHandler CoreExceptionHandler { get; }

        #endregion

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, ICoreRepository<TKey, TEntity,TErrorCoder> repository, IExceptionHandler<TErrorCoder> exceptionHandler)
        {
            UnitOfWork = unitOfWork;
            BusinessValidator = businessValidator;
            Repository = repository;

            ExceptionHandler = exceptionHandler;
            CoreExceptionHandler = ServiceLocator.Current.Get<ICoreExceptionHandler>();
        }

        #region Validations

        //protected virtual bool IsBusinessValid(List<ValidationRule> validationMethods)
        //{
        //    ValidationResult result = BusinessValidator.Validate(validationMethods);

        //    return result.IsValid;
        //}

        //protected ValidationResult ValidateBusiness(List<ValidationRule> validationMethods)
        //{
        //    ValidationResult validationResults = BusinessValidator.Validate(validationMethods);
        //    if (!validationResults.IsValid)
        //    {
        //        List<string> errors = new List<string>();
        //        string message = string.Empty;
        //        foreach (var validationFailure in validationResults.Errors)
        //        {
        //            message = validationFailure.Message;

        //            errors.Add(message);
        //            message = string.Empty;
        //        }

        //        BusinessValidationException valEx = new BusinessValidationException(errors);
        //        throw valEx;
        //    }
        //    return validationResults;
        //}

        #endregion

        #region CRUD

        public virtual IEnumerable<TEntity> Get<TFilter>(
        TFilter filter = default(TFilter),
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        bool includeDeleted = false,
        string includeProperties = "")
         where TFilter : class, IFilterExpression
        {
            try
            {
                var result = Repository.Get(filter, orderBy, includeDeleted, includeProperties);

                //TODO: Agregar configuración. Evaluar opcion de configurar por entidad o genérico.
                //if (result.Count() == 0)
                //{
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E404), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E404, message);
                //}
                return result;
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E400, typeof(TEntity));
            }
            return null;
        }

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
        )
        {
            try
            {
                var result = Repository.GetAll(orderBy, includeDeleted, includeProperties);

                //TODO: Agregar configuración. Evaluar opcion de configurar por entidad o genérico.
                //if (result.Count() == 0)
                //{
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E404), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E404, message);
                //}
                return result;
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E400, typeof(TEntity));
            }
            return null;
        }

        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                CheckId(id);

                var result = Repository.GetById(id, includeDeleted, includeProperties);

                if (result == null)
                {
                    CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(CoreErrors.E404, typeof(TEntity));
                }
                return result;
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E400, typeof(TEntity));
            }
            return null;
        }

        public TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                var result = Repository.GetEntity(filter, orderBy, includeDeleted, includeProperties);

                if (result == null)
                {
                    CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(CoreErrors.E404, typeof(TEntity));
                }
                return result;
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E400, typeof(TEntity));
            }
            return null;
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                CheckEntity(entity);

                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteAddValidationRules, BusinessValidator.AddValidationRules);
                if (!validationResult.IsValid)
                    CoreExceptionHandler.HandleException<BusinessValidationException>(validationResult);

                Repository.Insert(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E401, typeof(TEntity));
            }
        }

        public virtual void Update(TEntity entity)
        {
            CheckEntity(entity);
            CheckEntityId(entity);

            Update(entity.Id, entity);
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            try
            {
                CheckEntity(entity);
                CheckId(id);

                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteUpdateValidationRules, BusinessValidator.UpdateValidationRules);
                if (!validationResult.IsValid)
                    ExceptionHandler.HandleException<BusinessValidationException>(validationResult);

                entity.Id = id;
                Repository.Update(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E403, typeof(TEntity));
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                CheckEntity(entity);
                CheckEntityId(entity);

                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteDeleteValidationRules, BusinessValidator.DeleteValidationRules);
                if (!validationResult.IsValid)
                    ExceptionHandler.HandleException<BusinessValidationException>(validationResult);

                if (entity is ISoftDelete)
                {
                    ISoftDelete noDeletableEntity = entity as ISoftDelete;
                    noDeletableEntity.IsDeleted = true;
                    Repository.Update(entity);
                }
                else
                {
                    Repository.Delete(entity);
                }
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                CoreExceptionHandler.HandleException<BusinessException<CoreErrors>>(ex, CoreErrors.E402, typeof(TEntity));
            }
        }

        #endregion

        protected void CheckEntity(TEntity entity)
        {
            if (entity == default(TEntity)) throw new ArgumentException(nameof(entity));
        }
        protected void CheckEntityId(TEntity entity)
        {
            if (entity.Id.Equals(default(TKey))) throw new ArgumentException(nameof(entity.Id));
        }
        protected void CheckId(TKey id)
        {
            if (id.Equals(default(TKey))) throw new ArgumentException(nameof(id));
        }
    }


    public abstract class BusinessComponent<TEntity, TValidator, TErrorCoder> :
        BusinessComponent<int, TEntity, TValidator, TErrorCoder>,
        IBusinessComponent<TEntity, TValidator, TErrorCoder>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity, TErrorCoder>
        where TErrorCoder : Enum
    {

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, ICoreRepository<TEntity, TErrorCoder> repository, IExceptionHandler<TErrorCoder> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
        {
        }
    }

}
