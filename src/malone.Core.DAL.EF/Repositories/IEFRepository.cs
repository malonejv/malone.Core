using malone.Core.DAL.Base.Repositories;
using malone.Core.EL;
using malone.Core.EL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.EF.Repositories
{
    public interface IEFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
    }
}
