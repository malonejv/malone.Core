using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Business.Components
{
    public interface IBaseBusinessComponent<TEntity, TValidator>
        where TEntity : class
        where TValidator : IBaseBusinessValidator<TEntity>
    {

        #region Properties

        TValidator BusinessValidator { get; set; }

        IBaseRepository<TEntity> Repository { get; set; }

        #endregion

        #region CRUD

        IEnumerable<TEntity> GetAll(
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
            string includeProperties = ""
           );

        IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression;

        TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression;

        void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true);

        void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        #endregion

    }

    public interface IBusinessComponent<TKey, TEntity, TValidator> : IBaseBusinessComponent<TEntity, TValidator>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TValidator : IBusinessValidator<TKey, TEntity>
    {

        #region Properties

        //TValidator BusinessValidator { get; set; }

        new IRepository<TKey, TEntity> Repository { get; set; }

        #endregion

        #region CRUD

        //IEnumerable<TEntity> GetAll(
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //    string includeProperties = ""
        //   );

        //IEnumerable<TEntity> Get<TFilter>(
        //   TFilter filter = default(TFilter),
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //   string includeProperties = "")
        //    where TFilter : class, IFilterExpression;

        TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "");

        //TEntity GetEntity<TFilter>(
        //    TFilter filter = default(TFilter),
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = "")
        //    where TFilter : class, IFilterExpression;

        //void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        //void Update(TKey id, TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true);

        #endregion

    }

    public interface IBusinessComponent<TEntity, TValidator> : IBusinessComponent<int, TEntity, TValidator>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity>
    { }

}
