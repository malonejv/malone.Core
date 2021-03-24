using malone.Core.Entities.Model;
using System;
using System.Data.Entity;

namespace malone.Core.EF.Entities
{
    public static class IBaseEntityTKeyTEntityExtensions
    {
        public static EntityState AddOrUpdate<TKey, TEntity>(this TEntity entity)
            where TKey : IEquatable<TKey>
            where TEntity : IBaseEntity<TKey>
        {
            return entity.Id.Equals(default(TKey)) ? EntityState.Added : EntityState.Modified;
        }
    }

    public static class IBaseEntityExtensions
    {
        public static EntityState AddOrUpdate<TEntity>(this TEntity entity)
            where TEntity : IBaseEntity
        {
            return entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}
