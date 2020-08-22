using System;
using System.Threading.Tasks;
using malone.Core.AdoNet.Context;
using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Identity.AdoNet.Entities;

namespace malone.Core.DataAccess.EF.Repositories.Identity
{

    public class RoleRepository<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TContext> : AdoNetRepository<TKey, TRoleEntity>//RoleStore<TRoleEntity, TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
        where TContext : AdoNetDbContext
    {
        public RoleRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork,logger)
        {
        }


        /// <summary>
        ///     Find a role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<TRoleEntity> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }

        /// <summary>
        ///     Find a role by name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<TRoleEntity> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper() == roleName.ToUpper());
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual CreateAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Create(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="role"></param>
        public virtual DeleteAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Delete(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual UpdateAsync(TRoleEntity role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            this.Update(role, role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

    }

    public class RoleRepository<TContext> : RoleRepository<int, CoreUser, CoreRole, CoreUserLogin,CoreUserRole,CoreUserClaim, AdoNetIdentityDbContext>
        where TContext : AdoNetIdentityDbContext, IContext
    {
        private bool _disposed;

        public RoleRepository(TContext context) : base(context)
        {
        }

    }

}
