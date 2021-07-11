using Dapper;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities;
using malone.Core.Dapper.Entities.Filters;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Entities.Filters;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class RoleRepository<TKey, TRoleEntity, TUserRole> : CustomRepository<TRoleEntity>, IRoleRepository<TKey, TRoleEntity>
         where TKey : IEquatable<TKey>
         where TUserRole : CoreUserRole<TKey>, new()
         where TRoleEntity : CoreRole<TKey, TUserRole>, new()
    {
        public RoleRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        protected override TRoleEntity Map(DataRow row)
        {
            TRoleEntity userLogin = null;
            if (!row.IsNull())
            {
                userLogin = new TRoleEntity();
                userLogin.Id = row.AsTOrDefault<TKey>("Id");
                userLogin.Name = row.AsString("Name");
            }
            return userLogin;
        }

        #region Public Methods

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(TKey roleId)
        {
            string commandText = "Delete from Roles where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", roleId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public int Insert(TRoleEntity role)
        {
            string commandText = "Insert into Roles (Id, Name) values (@id, @name)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", role.Name);
            parameters.Add("@id", role.Id);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(TKey roleId)
        {
            string commandText = "Select Name from Roles where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", roleId);

            return Connection.QueryFirstOrDefault<string>(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public TKey GetRoleId(string roleName)
        {
            string commandText = "Select Id from Roles where Name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", roleName } };

            return Connection.QueryFirstOrDefault<TKey>(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Gets the TRoleEntity given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public TRoleEntity GetRoleById(TKey roleId)
        {
            var roleName = GetRoleName(roleId);
            TRoleEntity role = null;

            if (roleName != null)
            {
                role = new TRoleEntity()
                {
                    Name = roleName,
                    Id = roleId
                };
            }

            return role;

        }

        /// <summary>
        /// Gets the TRoleEntity given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public TRoleEntity GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            TRoleEntity role = null;

            if (roleId != null)
            {
                role = new TRoleEntity()
                {
                    Name = roleName,
                    Id = roleId
                };
            }

            return role;
        }

        public int Update(TRoleEntity role)
        {

            string commandText = "Update Roles set Name = @name where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", role.Name);
            parameters.Add("@id", role.Id);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        #endregion


    }

    public class RoleRepository<TRoleEntity, TUserRole> : RoleRepository<int, TRoleEntity, TUserRole>, IRoleRepository<TRoleEntity>
             where TUserRole : CoreUserRole, new()
             where TRoleEntity : CoreRole<TUserRole>, new()
    {
        public RoleRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
