using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;

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

        public ILogger Logger { get; set; }

        #endregion

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TKey, TEntity> repository, ILogger logger)
        {
            UnitOfWork = unitOfWork;
            BusinessValidator = businessValidator;
            Repository = repository;
            Logger = logger;
        }

        #region CRUD

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
        )
        {
            try
            {
                var result = Repository.GetAll(orderBy, includeDeleted, includeProperties);

                return result;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

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

                return result;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
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

                return result;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
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

                return result;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                CheckEntity(entity);

                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteAddValidationRules, BusinessValidator.AddValidationRules);
                if (!validationResult.IsValid)
                    throw new BusinessRulesValidationException(validationResult);

                Repository.Insert(entity);
                UnitOfWork.SaveChanges();
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS401, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            try
            {
                CheckEntity(entity);
                CheckId(id);

                var oldEntity = this.GetById(id);
                if (oldEntity.Equals(default(TEntity)))
                    throw CoreExceptionFactory.CreateException<EntityNotFoundException>(CoreErrors.BUSVAL500, typeof(TEntity), id);

                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteUpdateValidationRules, BusinessValidator.UpdateValidationRules);
                if (!validationResult.IsValid)
                    throw new BusinessRulesValidationException(validationResult);

                entity.Id = id;
                Repository.Update(oldEntity, entity);
                UnitOfWork.SaveChanges();
            }
            catch (EntityNotFoundException ex)
            {
                if (Logger != null) Logger.Warn(ex);

                throw;
            }
            catch (BusinessRulesValidationException ex)
            {
                if (Logger != null) Logger.Warn(ex);

                throw;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS403, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        public virtual void Delete(TKey id)
        {
            try
            {
                CheckId(id);


                var validationResult = BusinessValidator.Validate(BusinessValidator.ExecuteDeleteValidationRules, BusinessValidator.DeleteValidationRules);
                if (!validationResult.IsValid)
                    throw new BusinessRulesValidationException(validationResult);

                var entityToDelete = this.GetById(id);
                if (entityToDelete.Equals(default(TEntity)))
                    throw CoreExceptionFactory.CreateException<EntityNotFoundException>(CoreErrors.BUSVAL500, typeof(TEntity), id);

                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                {
                    var softDelete = entityToDelete as ISoftDelete;
                    softDelete.IsDeleted = true;
                    Repository.Update(entityToDelete, softDelete as TEntity);
                }
                else
                {
                    Repository.Delete(entityToDelete);
                }
                UnitOfWork.SaveChanges();
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS402, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
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

        public BusinessComponent(IUnitOfWork unitOfWork, TValidator businessValidator, IRepository<TEntity> repository, ILogger logger)
            : base(unitOfWork, businessValidator, repository, logger)
        {
        }
    }

}
