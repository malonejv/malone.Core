
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace malone.Core.DAL.Base.Context
{

    public interface IEFContext : IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        ChangeTracker ChangeTracker { get; }

    }
}
