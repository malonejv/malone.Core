using malone.Core.Business.Components;
using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Business.Components
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

        internal ICoreExceptionHandler CoreExceptionHandler { get; }

        #endregion

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TKey, TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            BusinessValidator = businessValidator;
            Repository = repository;

            CoreExceptionHandler = ServiceLocator.Current.Get<ICoreExceptionHandler>();
        }

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

                //TODO: Evaluar opcion de configurar por entidad o de forma generica la respuesta ante una consulta que no devuelve datos.
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

                //TODO: Evaluar opcion de configurar por entidad o de forma generica la respuesta ante una consulta que no devuelve datos.
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
                    CoreExceptionHandler.HandleException<BusinessValidationException>(validationResult);

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
                    CoreExceptionHandler.HandleException<BusinessValidationException>(validationResult);

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


    public abstract class BusinessComponent<TEntity, TValidator> : BusinessComponent<int, TEntity, TValidator>, IBusinessComponent<TEntity, TValidator>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity>
    {

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TEntity> repository)
            : base(unitOfWork, businessValidator, repository)
        {
        }
    }

}
