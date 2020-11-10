using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.Repositories
{
    public interface IRoleRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
    }

    public interface IRoleRepository<TEntity> : IRoleRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
