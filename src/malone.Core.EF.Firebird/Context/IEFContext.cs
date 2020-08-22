﻿using malone.Core.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EF.Firebird.Context
{

    public interface IEFContext : IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbChangeTracker ChangeTracker { get; }

    }
}
