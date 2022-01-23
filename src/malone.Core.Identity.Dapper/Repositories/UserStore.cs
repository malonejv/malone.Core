﻿using malone.Core.AdoNet.Context;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.Entities.Model;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.Identity.Dapper.Entities.Filters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace malone.Core.Identity.Dapper.Repositories
{
    public abstract class UserStore<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> :
        IUserLoginStore<TUserEntity, TKey>,
        IUserClaimStore<TUserEntity, TKey>,
        IUserRoleStore<TUserEntity, TKey>,
        IUserPasswordStore<TUserEntity, TKey>,
        IUserSecurityStampStore<TUserEntity, TKey>,
        IQueryableUserStore<TUserEntity, TKey>,
        IUserEmailStore<TUserEntity, TKey>,
        IUserPhoneNumberStore<TUserEntity, TKey>,
        IUserTwoFactorStore<TUserEntity, TKey>,
        IUserLockoutStore<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, IBaseEntity<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {

        protected IUserLoginRepository<TKey, TUserLogin> _logins;
        protected IUserClaimRepository<TKey, TUserClaim> _userClaims;
        protected IUserRoleRepository<TKey, TUserRole> _userRoles;
        protected IRoleRepository<TKey, TRoleEntity> _roles;
        protected IUserRepository<TKey, TUserEntity> _users;

        protected IContext Context { get; private set; }

        protected ILogger Logger { get; }

        /// <summary>
        ///     If true will call SaveChanges after Create/Update/Delete
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        public UserStore(IUserLoginRepository<TKey, TUserLogin> logins, IUserClaimRepository<TKey, TUserClaim> userClaims, IUserRoleRepository<TKey, TUserRole> userRoles, IRoleRepository<TKey, TRoleEntity> roles, IUserRepository<TKey, TUserEntity> users, IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            _logins = logins;
            _userClaims = userClaims;
            _userRoles = userRoles;
            _roles = roles;
            _users = users;
            Context = context;
            Logger = logger;

            AutoSaveChanges = true;
        }

        #region IQueryableUserStore

        public IQueryable<TUserEntity> Users => throw new NotImplementedException();

        #endregion

        #region IUserLoginStore

        public async Task AddLoginAsync(TUserEntity user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            login.ThrowIfNull(nameof(login));

            await Task.Run(() => { _logins.Insert(user.Id, login); }).ConfigureAwait(false);
        }

        public async Task RemoveLoginAsync(TUserEntity user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            login.ThrowIfNull(nameof(login));

            await Task.Run(() => { _logins.Delete(user.Id, login); }).ConfigureAwait(false);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            List<UserLoginInfo> logins = _logins.FindByUserId(user.Id);

            return await Task.FromResult<IList<UserLoginInfo>>(logins).ConfigureAwait(false);
        }

        public async Task<TUserEntity> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            login.ThrowIfNull(nameof(login));

            TKey userId = _logins.FindUserIdByLogin<TKey>(login);
            TUserEntity user = null;
            if (!userId.IsDefault())
            {
                user = _users.GetUserById(userId) as TUserEntity;
            }
            return await Task.FromResult<TUserEntity>(user).ConfigureAwait(false);
        }

        public async Task CreateAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            _users.Insert(user);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            _users.Update(user);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            _users.Delete(user);
            await SaveChangesAsync();
        }

        public async Task<TUserEntity> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            TUserEntity result = _users.GetUserById(userId) as TUserEntity;
            if (!result.IsDefault())
            {
                return await Task.FromResult<TUserEntity>(result).ConfigureAwait(false);
            }

            return await Task.FromResult<TUserEntity>(null).ConfigureAwait(false);
        }

        public async Task<TUserEntity> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            userName.ThrowIfNullOrEmpty(nameof(userName));

            List<TUserEntity> result = _users.GetUserByName(userName) as List<TUserEntity>;

            // Should I throw if > 1 user?
            if (result != null && result.Count == 1)
            {
                return await Task.FromResult<TUserEntity>(result[0]).ConfigureAwait(false);
            }

            return await Task.FromResult<TUserEntity>(null).ConfigureAwait(false);
        }

        #endregion

        #region IUserClaimStore

        public async Task<IList<Claim>> GetClaimsAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            ClaimsIdentity identity = _userClaims.FindByUserId(user.Id);

            return await Task.FromResult<IList<Claim>>(identity.Claims.ToList()).ConfigureAwait(false);
        }

        public async Task AddClaimAsync(TUserEntity user, Claim claim)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            claim.ThrowIfNull(nameof(claim));

            await Task.Run(() =>
            {
                _userClaims.Insert(user.Id, claim);
            }).ConfigureAwait(false);
        }

        public async Task RemoveClaimAsync(TUserEntity user, Claim claim)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            claim.ThrowIfNull(nameof(claim));

            await Task.Run(() =>
            {
                _userClaims.Delete(user.Id, claim);
            }).ConfigureAwait(false);
        }

        #endregion

        #region IUserRoleStore

        public async Task AddToRoleAsync(TUserEntity user, string roleName)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            roleName.ThrowIfNullOrEmpty(nameof(roleName));

            await Task.Run(() =>
            {
                var roleId = _roles.GetRoleId(roleName);
                if (!roleId.IsDefault())
                {
                    _userRoles.Insert(user.Id, roleId);
                }
            }).ConfigureAwait(false);
        }

        public async Task RemoveFromRoleAsync(TUserEntity user, string roleName)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            roleName.ThrowIfNullOrEmpty(nameof(roleName));

            await Task.Run(() =>
            {
                var roleId = _roles.GetRoleId(roleName);
                if (!roleId.IsDefault())
                {
                    _userRoles.Delete(user.Id, roleId);
                }
            }).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetRolesAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));


            List<string> roles = _userRoles.FindByUserId(user.Id);
            {
                if (roles != null)
                {
                    return await Task.FromResult<IList<string>>(roles).ConfigureAwait(false);
                }
            }

            return await Task.FromResult<IList<string>>(null).ConfigureAwait(false);
        }

        public async Task<bool> IsInRoleAsync(TUserEntity user, string roleName)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));
            roleName.ThrowIfNullOrEmpty(nameof(roleName));

            List<string> roles = _userRoles.FindByUserId(user.Id);
            {
                if (roles != null && roles.Contains(roleName))
                {
                    return await Task.FromResult<bool>(true);
                }
            }

            return await Task.FromResult<bool>(false);
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(TUserEntity user, string passwordHash)
        {

            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUserEntity user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region IUserSecurityStampStore

        public Task SetSecurityStampAsync(TUserEntity user, string stamp)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(TUserEntity user, string email)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUserEntity user, bool confirmed)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<TUserEntity> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            email.ThrowIfNullOrEmpty(nameof(email));

            TUserEntity result = _users.GetUserByEmail(email) as TUserEntity;
            if (result != null)
            {
                return Task.FromResult<TUserEntity>(result);
            }

            return Task.FromResult<TUserEntity>(null);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(TUserEntity user, string phoneNumber)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUserEntity user, bool confirmed)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        #endregion

        #region IUserTwoFactorStore

        public Task SetTwoFactorEnabledAsync(TUserEntity user, bool enabled)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region IUserLockoutStore

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(TUserEntity user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUserEntity user)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUserEntity user, bool enabled)
        {
            ThrowIfDisposed();
            user.ThrowIfNull(nameof(user));

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
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

        // Only call save changes if AutoSaveChanges is true
        private async Task SaveChangesAsync()
        {
            await Task.Run(() =>
            {
                if (AutoSaveChanges)
                {
                    Context.SaveChanges();
                }
            }).ConfigureAwait(false);
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

        #region Inner Class

        //Evaluar usar este tipo de objetos para reemplazar los IAdoNetFilterExpression

        // We want to use FindAsync() when looking for an User.Id instead of LINQ to avoid extra 
        // database roundtrips. This class cracks open the filter expression passed by 
        // UserStore.FindByIdAsync() to obtain the value of the id we are looking for 
        private static class FindByIdFilterParser
        {
            // expression pattern we need to match
            private static readonly Expression<Func<TUserEntity, bool>> Predicate = u => u.Id.Equals(default(TKey));
            // method we need to match: Object.Equals() 
            private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)Predicate.Body).Method;
            // property access we need to match: User.Id 
            private static readonly MemberInfo UserIdMemberInfo = ((MemberExpression)((MethodCallExpression)Predicate.Body).Object).Member;

            internal static bool TryMatchAndGetId(Expression<Func<TUserEntity, bool>> filter, out TKey id)
            {
                // default value in case we can’t obtain it 
                id = default(TKey);

                // lambda body should be a call 
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }

                // actually a call to object.Equals(object)
                var callExpression = (MethodCallExpression)filter.Body;
                if (callExpression.Method != EqualsMethodInfo)
                {
                    return false;
                }
                // left side of Equals() should be an access to User.Id
                if (callExpression.Object == null
                    || callExpression.Object.NodeType != ExpressionType.MemberAccess
                    || ((MemberExpression)callExpression.Object).Member != UserIdMemberInfo)
                {
                    return false;
                }

                // There should be only one argument for Equals()
                if (callExpression.Arguments.Count != 1)
                {
                    return false;
                }

                MemberExpression fieldAccess;
                if (callExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    // convert node should have an member access access node
                    // This is for cases when primary key is a value type
                    var convert = (UnaryExpression)callExpression.Arguments[0];
                    if (convert.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    fieldAccess = (MemberExpression)convert.Operand;
                }
                else if (callExpression.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    // Get field member for when key is reference type
                    fieldAccess = (MemberExpression)callExpression.Arguments[0];
                }
                else
                {
                    return false;
                }

                // and member access should be a field access to a variable captured in a closure
                if (fieldAccess.Member.MemberType != MemberTypes.Field
                    || fieldAccess.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }

                // expression tree matched so we can now just get the value of the id 
                var fieldInfo = (FieldInfo)fieldAccess.Member;
                var closure = ((ConstantExpression)fieldAccess.Expression).Value;

                id = (TKey)fieldInfo.GetValue(closure);
                return true;
            }
        }

        // We want to use FindAsync() when looking for an User.UserName instead of LINQ to avoid extra 
        // database roundtrips. This class cracks open the filter expression passed by 
        // UserStore.FindByIdAsync() to obtain the value of the id we are looking for 
        private static class FindByUserNameOrEmailFilterParser
        {
            // expression pattern we need to match
            private static readonly Expression<Func<TUserEntity, bool>> UserNamePredicate = u => u.UserName.Equals(null);
            // method we need to match: Object.Equals() 
            private static readonly MethodInfo UserNameEqualsMethodInfo = ((MethodCallExpression)UserNamePredicate.Body).Method;
            // property access we need to match: UserRequest.UserNameOrEmail 
            private static readonly MemberInfo UserNameMemberInfo = ((MemberExpression)((MethodCallExpression)UserNamePredicate.Body).Object).Member;

            // expression pattern we need to match
            private static readonly Expression<Func<TUserEntity, bool>> EmailPredicate = u => u.Email.Equals(null);
            // method we need to match: Object.Equals() 
            private static readonly MethodInfo EmailEqualsMethodInfo = ((MethodCallExpression)EmailPredicate.Body).Method;
            // property access we need to match: UserRequest.UserNameOrEmail 
            private static readonly MemberInfo EmailMemberInfo = ((MemberExpression)((MethodCallExpression)EmailPredicate.Body).Object).Member;


            internal static bool TryMatchAndGetUserNameOrEmail(Expression<Func<TUserEntity, bool>> filter, out string userNameOrEmail)
            {
                // default value in case we can’t obtain it 
                userNameOrEmail = null;

                // lambda body should be a call 
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }

                // actually a call to object.Equals(object)
                var callExpression = (MethodCallExpression)filter.Body;
                if (callExpression.Method != UserNameEqualsMethodInfo &&
                    callExpression.Method != EmailEqualsMethodInfo)
                {
                    return false;
                }

                // left side of Equals() should be an access to UserRequest.UserNameOrEmail
                if (callExpression.Object == null
                    || callExpression.Object.NodeType != ExpressionType.MemberAccess
                    || (
                          (((MemberExpression)callExpression.Object).Member != UserNameMemberInfo) &&
                          (((MemberExpression)callExpression.Object).Member != EmailMemberInfo)
                       )
                   )
                {
                    return false;
                }

                // There should be only one argument for Equals()
                if (callExpression.Arguments.Count != 1)
                {
                    return false;
                }

                MemberExpression fieldAccess;
                if (callExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    // convert node should have an member access access node
                    // This is for cases when primary key is a value type
                    var convert = (UnaryExpression)callExpression.Arguments[0];
                    if (convert.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    fieldAccess = (MemberExpression)convert.Operand;
                }
                else if (callExpression.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    // Get field member for when key is reference type
                    fieldAccess = (MemberExpression)callExpression.Arguments[0];
                }
                else
                {
                    return false;
                }

                // and member access should be a field access to a variable captured in a closure
                if (fieldAccess.Member.MemberType != MemberTypes.Field
                    || fieldAccess.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }

                // expression tree matched so we can now just get the value of the id 
                var fieldInfo = (FieldInfo)fieldAccess.Member;
                var closure = ((ConstantExpression)fieldAccess.Expression).Value;

                userNameOrEmail = (string)fieldInfo.GetValue(closure);
                return true;
            }
        }

        #endregion

    }

    public class UserStore<TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : UserStore<int, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
            where TUserLogin : CoreUserLogin, new()
            where TUserClaim : CoreUserClaim, IBaseEntity, new()
            where TUserRole : CoreUserRole, new()
            where TRoleEntity : CoreRole<TUserRole>
            where TUserEntity : CoreUser<TUserLogin, TUserRole, TUserClaim>
    {
        public UserStore(IUserLoginRepository<TUserLogin> logins, IUserClaimRepository<TUserClaim> userClaims, IUserRoleRepository<TUserRole> userRoles, IRoleRepository<TRoleEntity> roles, IUserRepository<TUserEntity> users, IContext context, ILogger logger) : base(logins, userClaims, userRoles, roles, users, context, logger)
        {
        }
    }

}