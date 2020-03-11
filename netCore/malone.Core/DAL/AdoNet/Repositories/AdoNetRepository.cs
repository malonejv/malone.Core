using malone.Core.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.DAL.AdoNet.Context;
using malone.Core.DAL.Base.Repositories;
using malone.Core.EL.Filters;

namespace malone.Core.DAL.AdoNet.Repositories
{
    public abstract class AdoNetRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected AdoNetContext _context;
        //protected IDbCommand _command;
        
        protected IUnitOfWork UnitOfWork { get; private set; }


        public AdoNetRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            UnitOfWork = unitOfWork;
            _context = (AdoNetContext)UnitOfWork.Context;
        }

        public abstract IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilter;

        public abstract IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           );

        public abstract TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "");

        public abstract TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilter;

        public abstract void Insert(TEntity entity);

        public abstract void Delete(object id);

        public abstract void Delete(TEntity entityToDelete);

        public abstract void Update(TEntity entityToUpdate);
        
    }
}
