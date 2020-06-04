using ErgaOmnes.Core.CL.Exceptions;
using malone.Core.DAL.Repositories;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.DAL.Repositories
{
    public interface IRepository<TEntity> : ICoreRepository<TEntity, ErrorCodes>
        where TEntity : class, IBaseEntity
    {
    }
}
