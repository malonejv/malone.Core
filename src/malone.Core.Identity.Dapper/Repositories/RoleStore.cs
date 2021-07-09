using malone.Core.AdoNet.Context;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.Identity.Dapper.Entities.Filters;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class RoleStore<TKey, TRoleEntity, TUserRole> : IQueryableRoleStore<TRoleEntity, TKey>
        where TKey : IEquatable<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>, new()
        where TUserRole : CoreUserRole<TKey>
    {
        protected IRoleRepository<TKey, TRoleEntity> _roles;

        protected IContext Context { get; private set; }

        protected ILogger Logger { get; }

        public RoleStore(IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            Context = context;
            Logger = logger;
        }


        #region IQueryableRoleStore

        public IQueryable<TRoleEntity> Roles => _roles.GetAll().AsQueryable<TRoleEntity>();

        public async Task<TRoleEntity> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roles.GetById(roleId);
        }

        public async Task<TRoleEntity> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roles.GetEntity(new RoleGetRequest()
            {
                Name = roleName
            });
        }

        public async Task CreateAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            role.ThrowIfNull(nameof(role));

            _roles.Insert(role);
            await Task.FromResult(Context.SaveChanges());
        }

        public async Task UpdateAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            role.ThrowIfNull(nameof(role));

            _roles.Update(role);
            await Task.FromResult(Context.SaveChanges());
        }

        public async Task DeleteAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            role.ThrowIfNull(nameof(role));

            _roles.Delete(role);
            await Task.FromResult(Context.SaveChanges());
        }

        #endregion

        #region Private methods

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
                throw new ObjectDisposedException(GetType().Name);
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

    public class RoleStore<TRoleEntity, TUserRole> : RoleStore<int, TRoleEntity, TUserRole>
        where TRoleEntity : CoreRole<TUserRole>, new()
        where TUserRole : CoreUserRole
    {

        public RoleStore(IContext context, ILogger logger) : base(context, logger)
        {
        }

    }

}