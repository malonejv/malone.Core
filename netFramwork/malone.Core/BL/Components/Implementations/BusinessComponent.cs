using malone.Core.BL.Components.Interfaces;
using malone.Core.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using malone.Core.DAL.Base.Repositories;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.EL.Filters;
using malone.Core.CL.Log;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.CL.Exceptions.Handler.Interfaces;

namespace malone.Core.BL.Components.Implementations
{
    public abstract class BusinessComponent<TEntity, TValidator> : IBusinessComponent<TEntity, TValidator>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity>
    {

        #region Properties

        protected IRepository<TEntity> Repository { get; private set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public TValidator BusinessValidator { get; set; }

        protected IExceptionMessageManager MessageManager { get; }

        protected IExceptionHandler ExceptionHandler { get; }

        #endregion

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TEntity> repository, IExceptionMessageManager exManager, IExceptionHandler exHandler)
        {
            UnitOfWork = unitOfWork;
            BusinessValidator = businessValidator;
            Repository = repository;

            MessageManager = exManager;
            ExceptionHandler = exHandler;
        }

        #region Validations

        protected virtual bool IsBusinessValid(List<ValidationRule> validationMethods)
        {
            ValidationResult result = BusinessValidator.Validate(validationMethods);

            return result.IsValid;
        }

        protected ValidationResult ValidateBusiness(List<ValidationRule> validationMethods)
        {
            ValidationResult validationResults = BusinessValidator.Validate(validationMethods);
            if (!validationResults.IsValid)
            {
                List<string> errors = new List<string>();
                string message = string.Empty;
                foreach (var validationFailure in validationResults.Errors)
                {
                    //if (validationFailure.ErrorCode.HasValue)
                    //    //message = string.Format("Error {0}: {1}", validationFailure.ErrorCode.Value, validationFailure.Message);
                    //else
                    message = validationFailure.Message;

                    errors.Add(message);
                    message = string.Empty;
                }

                BusinessValidationException valEx = new BusinessValidationException(errors);
                throw valEx;
            }
            return validationResults;
        }

        #endregion

        #region CRUD

        public virtual IEnumerable<TEntity> Get<TFilter>(
        TFilter filter = default(TFilter),
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        bool includeDeleted = false,
        string includeProperties = "")
         where TFilter : class, IFilter
        {
            try
            {
                var result = Repository.Get(filter, orderBy, includeDeleted, includeProperties);

                //if (result.Count() == 0)
                //{
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E304, message);
                //}
                return result;
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E300, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E300, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
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

                //if (result.Count() == 0)
                //{
                //    var message = string.Format(messageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                //    throw new BusinessException((int)CoreErrors.E304, message);
                //}
                return result;
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E300, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E300, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
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
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                    throw new BusinessException((int)CoreErrors.E304, message);
                }
                return result;
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E300, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E300, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
            }
            return null;
        }

        public TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilter
        {
            try
            {
                var result = Repository.GetEntity(filter, orderBy, includeDeleted, includeProperties);

                if (result == null)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E304), typeof(TEntity));
                    throw new BusinessException((int)CoreErrors.E304, message);
                }
                return result;
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E300, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E300), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E300, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
            }
            return null;
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                ValidateBusiness(BusinessValidator.AddValidationRules);
                Repository.Insert(entity);
                UnitOfWork.SaveChanges();
            }
            catch (BusinessValidationException valEx)
            {
                ExceptionHandler.HandleException(valEx);
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E301), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E301, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E301), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E301, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                ValidateBusiness(BusinessValidator.UpdateValidationRules);
                Repository.Update(entity);
                UnitOfWork.SaveChanges();
            }
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E303), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E303, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E303), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E303, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                ValidateBusiness(BusinessValidator.DeleteValidationRules);
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
            catch (DataAccessException dex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E302), typeof(TEntity));
                var bex = new BusinessException((int)CoreErrors.E302, message, dex);
                ExceptionHandler.HandleException(bex);
            }
            catch (Exception ex)
            {
                var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
                if (!isBaseException)
                {
                    var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E302), typeof(TEntity));
                    var bex = new BusinessException((int)CoreErrors.E302, message, ex, true);
                    ExceptionHandler.HandleException(bex);
                }
                else
                {
                    var bex = (BaseException)ex;
                    if (bex.Rethrow)
                        throw bex;
                }
            }
        }

        #endregion
    }
}
