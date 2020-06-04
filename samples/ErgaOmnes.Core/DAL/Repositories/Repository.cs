using ErgaOmnes.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EF.DAL.Repositories.Implementations;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.DAL.Repositories
{
    public class Repository<TEntity> : EFRepository<TEntity, ErrorCodes>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IUnitOfWork unitOfWork, IExceptionHandler<ErrorCodes> exceptionHandler) : base(unitOfWork, exceptionHandler)
        {
        }
    }
}
