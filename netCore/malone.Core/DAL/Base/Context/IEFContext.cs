﻿namespace malone.Core.DAL.Base.Context
{

    public interface IEFContext : IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbChangeTracker ChangeTracker { get; }

    }
}
