using malone.Core.EL.Filters;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.BL.Components.Interfaces
{
    public interface IBusinessComponent<TKey, TEntity, TValidator>
        where TEntity : class, IBaseEntity<TKey>
        where TValidator : IBusinessValidator<TKey, TEntity>
    {

        #region Properties
        TValidator BusinessValidator { get; set; }
        #endregion

        #region CRUD

        IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression;

        IEnumerable<TEntity> GetAll(
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
            string includeProperties = ""
           );

        TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "");

        TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression;

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        #endregion

    }

    public interface IBusinessComponent<TEntity, TValidator> : IBusinessComponent<int, TEntity, TValidator>
       where TEntity : class, IBaseEntity
       where TValidator : IBusinessValidator<TEntity>
    { }

}
