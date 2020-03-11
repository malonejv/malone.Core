using malone.Core.EL;
using malone.Core.EL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.BL.Components.Interfaces
{
    public interface IBusinessComponent<TEntity, TValidator>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity>
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
            where TFilter : class, IFilter;

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
            where TFilter : class, IFilter;

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        #endregion

    }

}
