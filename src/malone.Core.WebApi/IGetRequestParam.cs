using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.WebApi
{
    public interface IGetRequestParam<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
    }
    public interface IGetRequestParam<TEntity> : IGetRequestParam<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
