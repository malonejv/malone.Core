﻿using Dapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class UserRoleRepository<TKey, TUserRole> : CustomRepository<TUserRole>, IUserRoleRepository<TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>, new()
    {
        public UserRoleRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        protected override TUserRole Map(DataRow row)
        {
            TUserRole userLogin = null;
            if (!row.IsNull())
            {
                userLogin = new TUserRole();
                userLogin.RoleId = row.AsTOrDefault<TKey>("RoleId");
                userLogin.UserId = row.AsTOrDefault<TKey>("UserId");
            }
            return userLogin;
        }

        #region Public Methods

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>
        {
            List<string> roles = new List<string>();
            string commandText = "Select Roles.Name from UsersRoles, Roles where UsersRoles.UserId = @userId and UsersRoles.RoleId = Roles.Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@userId", userId);

            var result = Connection.Query<string>(commandText, parameters, transaction: Context.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete<TUserKey, TRoleKey>(TUserKey userId, TRoleKey roleId)
            where TUserKey : IEquatable<TUserKey>
            where TRoleKey : IEquatable<TRoleKey>
        {
            string commandText = "Delete from UsersRoles where UserId = @userId and  RoleId = @roleId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("UserId", userId);
            parameters.Add("RoleId", roleId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public int Insert<TUserKey, TRoleKey>(TUserKey userId, TRoleKey roleId)
            where TUserKey : IEquatable<TUserKey>
            where TRoleKey : IEquatable<TRoleKey>
        {
            string commandText = "Insert into UsersRoles (UserId, RoleId) values (@userId, @roleId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("userId", userId);
            parameters.Add("roleId", roleId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        #endregion

    }

    public class UserRoleRepository<TUserRole> : UserRoleRepository<int, TUserRole>, IUserRoleRepository<TUserRole>
        where TUserRole : CoreUserRole, new()
    {
        public UserRoleRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
