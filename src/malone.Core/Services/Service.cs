﻿//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:39</date>

using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Services
	{
	public abstract class BaseService<TEntity, TValidator> : IBaseService<TEntity, TValidator>
        where TEntity : class
        where TValidator : IBaseServiceValidator<TEntity>
    {
        public IBaseRepository<TEntity> Repository { get; set; }

        public TValidator ServiceValidator { get; set; }

        public ICoreLogger Logger { get; set; }

        public BaseService(TValidator businessValidator, IBaseRepository<TEntity> repository, ICoreLogger logger)
        {
            ServiceValidator = businessValidator;
            Repository = repository;
            Logger = logger;
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

                return result;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

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
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

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
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        public virtual void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
        {
            try
            {
                var uow = UnitOfWork.Create();

                CheckEntity(entity);

                var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteAddValidationRules, ServiceValidator.AddValidationRules);
                if (!validationResult.IsValid)
                {
                    throw new BusinessRulesValidationException(validationResult);
                }

                Repository.Insert(entity);

                if (saveChanges)
                {
                    uow.SaveChanges();
                }

                if (disposeUoW)
                {
                    uow.Dispose();
                }
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS401, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        public virtual void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true)
        {
            try
            {
                var uow = UnitOfWork.Create();
                CheckEntity(oldValues);
                CheckEntityId(oldValues);
                CheckEntity(newValues);

                var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteUpdateValidationRules, ServiceValidator.UpdateValidationRules);
                if (!validationResult.IsValid)
                {
                    throw new BusinessRulesValidationException(validationResult);
                }

                Repository.Update(oldValues, newValues);
                if (saveChanges)
                {
                    uow.SaveChanges();
                }

                if (disposeUoW)
                {
                    uow.Dispose();
                }
            }
            catch (EntityNotFoundException ex)
            {
                if (Logger != null)
                {
                    Logger.Warn(ex);
                }

                throw;
            }
            catch (BusinessRulesValidationException ex)
            {
                if (Logger != null)
                {
                    Logger.Warn(ex);
                }

                throw;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS403, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        public virtual void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
        {
            try
            {
                var uow = UnitOfWork.Create();
                CheckEntity(entity);
                CheckEntityId(entity);

                var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteDeleteValidationRules, ServiceValidator.DeleteValidationRules);
                if (!validationResult.IsValid)
                {
                    throw new BusinessRulesValidationException(validationResult);
                }

                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                {
                    var softDelete = entity as ISoftDelete;
                    softDelete.IsDeleted = true;
                    Repository.Update(entity, softDelete as TEntity);
                }
                else
                {
                    Repository.Delete(entity);
                }

                if (saveChanges)
                {
                    uow.SaveChanges();
                }

                if (disposeUoW)
                {
                    uow.Dispose();
                }
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS402, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        protected void CheckEntity(TEntity entity)
        {
            if (entity == default(TEntity))
            {
                throw new ArgumentException(nameof(entity));
            }
        }

        protected abstract void CheckEntityId(TEntity entity);

        protected abstract void CheckId(params object[] args);
    }

    public abstract class Service<TKey, TEntity, TValidator> : BaseService<TEntity, TValidator>, IService<TKey, TEntity, TValidator>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
where TValidator : IServiceValidator<TKey, TEntity>
    {
        public IRepository<TKey, TEntity> Repository { get; set; }

        public TValidator ServiceValidator { get; set; }

        public ICoreLogger Logger { get; set; }

        public Service(TValidator businessValidator, IRepository<TKey, TEntity> repository, ICoreLogger logger) : base(businessValidator, repository, logger)
        {
            ServiceValidator = businessValidator;
            Repository = repository;
            Logger = logger;
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
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        public virtual void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
        {
            try
            {
                var uow = UnitOfWork.Create();
                CheckEntity(entity);
                CheckEntityId(entity);

                var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteUpdateValidationRules, ServiceValidator.UpdateValidationRules);
                if (!validationResult.IsValid)
                {
                    throw new BusinessRulesValidationException(validationResult);
                }

                Repository.Update(entity);
                if (saveChanges)
                {
                    uow.SaveChanges();
                }

                if (disposeUoW)
                {
                    uow.Dispose();
                }
            }
            catch (EntityNotFoundException ex)
            {
                if (Logger != null)
                {
                    Logger.Warn(ex);
                }

                throw;
            }
            catch (BusinessRulesValidationException ex)
            {
                if (Logger != null)
                {
                    Logger.Warn(ex);
                }

                throw;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS403, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        public virtual void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true)
        {
            try
            {
                var uow = UnitOfWork.Create();
                CheckId(id);

                var entity = this.GetById(id);
                if (entity == default(TEntity))
                {
                    throw CoreExceptionFactory.CreateException<EntityNotFoundException>(CoreErrors.BUSVAL500, typeof(TEntity), id);
                }

                Delete(entity, saveChanges, disposeUoW);
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS402, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        protected void CheckEntity(TEntity entity)
        {
            if (entity == default(TEntity))
            {
                throw new ArgumentException(nameof(entity));
            }
        }

        protected override void CheckEntityId(TEntity entity)
        {
            if (entity.Id.Equals(default(TKey)))
            {
                throw new ArgumentException(nameof(entity.Id));
            }
        }

        protected void CheckId(TKey id)
        {
            if (id.Equals(default(TKey)))
            {
                throw new ArgumentException(nameof(id));
            }
        }

        protected override void CheckId(params object[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length == 0)
            {
                throw new ArgumentException(nameof(args));
            }

            int id = (int)args[0];

            if (id.Equals(default(TKey)))
            {
                throw new ArgumentException(nameof(id));
            }
        }
    }

    public abstract class Service<TEntity, TValidator> : Service<int, TEntity, TValidator>, IService<TEntity, TValidator>
where TEntity : class, IBaseEntity
where TValidator : IServiceValidator<TEntity>
    {
        public Service(TValidator businessValidator, IRepository<TEntity> repository, ICoreLogger logger)
: base(businessValidator, repository, logger)
        {
        }
    }
}