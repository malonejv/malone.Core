using malone.Core.AdoNet.Context;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Dapper.Repositories
{
    /// <summary>
    /// Creates BaseBaseRepository
    /// </summary>
    public class CustomRepository<TEntity> : ICustomRepository<TEntity>, IDisposable
        where TEntity : class
    {
        protected CoreDbContext Context { get; private set; }
        protected IDbConnection Connection => Context.Connection;
        protected ILogger Logger { get; }

        protected Type TEntityType { get; set; }

        #region Constructor

        public CustomRepository(IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            Context = (CoreDbContext)context;
            Logger = logger;
            TEntityType = this.GetType().GetInterface("ICustomRepository`1").GetGenericArguments()[0];
        }

        #endregion

        #region Private And Protected Methods

        private void CheckLogger(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
        }

        private void CheckContext(IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!(context is CoreDbContext))
            {
                //TODO: Implementar excepciones del core
                throw new ArgumentException();
            }
        }

        #endregion

        #region Dispose

        protected bool _disposed;

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
        }

        #endregion
    }
}
