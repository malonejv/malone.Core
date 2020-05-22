using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EL.Filters;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.BL.Components.Implementations
{
    public abstract class BusinessComponent<TKey, TEntity, TValidator> : IBusinessComponent<TKey, TEntity, TValidator>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TValidator : IBusinessValidator<TKey, TEntity> 
    {

        #region Properties

        protected IRepository<TKey, TEntity> Repository { get; private set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public TValidator BusinessValidator { get; set; }

        protected IExceptionHandler<CoreErrors> ExceptionHandler { get; }

        #endregion

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TKey, TEntity> repository, IExceptionHandler<CoreErrors> exceptionHandler)
        {
            UnitOfWork = unitOfWork;
            BusinessValidator = businessValidator;
            Repository = repository;

            ExceptionHandler = exceptionHandler;
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
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E304, message);
                //}
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E300, typeof(TEntity));
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
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E304, message);
                //}
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E300, typeof(TEntity));
            }
            return null;
        }

        public virtual TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                var result = Repository.GetById(id, includeDeleted, includeProperties);

                if (result == null)
                {
                    ExceptionHandler.HandleException<BusinessException>( CoreErrors.E304, typeof(TEntity));
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E300, typeof(TEntity));
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
                    ExceptionHandler.HandleException<BusinessException>(CoreErrors.E304, typeof(TEntity));
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E300, typeof(TEntity));
            }
            return null;
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                var validationResult =  BusinessValidator.Validate(BusinessValidator.ExecuteAddValidationRules, BusinessValidator.AddValidationRules);
                if(!validationResult.IsValid)
                    ExceptionHandler.HandleException<BusinessValidationException>(validationResult);

                Repository.Insert(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E301, typeof(TEntity));
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteUpdateValidationRules,BusinessValidator.UpdateValidationRules);
                if (!validationResult.IsValid)
                    ExceptionHandler.HandleException<BusinessValidationException>(validationResult);
                Repository.Update(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E303, typeof(TEntity));
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteDeleteValidationRules,BusinessValidator.DeleteValidationRules);
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
                ExceptionHandler.HandleException<BusinessException>(ex, CoreErrors.E302, typeof(TEntity));
            }
        }

        #endregion
    }


    public abstract class BusinessComponent<TEntity, TValidator> : BusinessComponent<int, TEntity, TValidator>, IBusinessComponent<TEntity, TValidator>
   where TEntity : class, IBaseEntity
   where TValidator : IBusinessValidator<TEntity>
    {

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TEntity> repository, IExceptionHandler<CoreErrors> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
        {
        }
    }

}
