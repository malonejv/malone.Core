//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using malone.Core.Entities.Model;
using System;

namespace malone.Core.DataAccess.Repositories
{
                        public interface IRepository<TKey, TEntity> : IBaseRepository<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
                                                                TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "");

                                        void Update(TEntity entity);
    }

                    public interface IRepository<TEntity> : IRepository<int, TEntity>
        where TEntity : class, IBaseEntity
    {
    }
}
