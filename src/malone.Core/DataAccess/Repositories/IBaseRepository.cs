//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using malone.Core.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DataAccess.Repositories
{
                    public interface IBaseRepository<T>
        where T : class
    {
                                                                                IEnumerable<T> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression;

                                                                IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           );

                                                                                T GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression;

                                        void Insert(T entity);

                                        void Delete(T entity);

                                                void Update(T oldValues, T newValues);
    }
}
