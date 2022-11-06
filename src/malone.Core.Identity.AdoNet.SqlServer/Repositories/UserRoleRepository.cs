using System;
using System.Data;
using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.AdoNet.SqlServer.Entities;
using malone.Core.Logging;

namespace malone.Core.Identity.AdoNet.SqlServer.Repositories
{
	public class UserRoleRepository<TKey, TUserRole> : BaseRepository<TUserRole>, IUserRoleRepository<TKey, TUserRole>
		where TKey : IEquatable<TKey>
		where TUserRole : CoreUserRole<TKey>, new()
	{
		public UserRoleRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}

		#region Overridden Methods

		#region Crud Operations

		#region Get

		protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted)
		{
			string query = @"SELECT UserId, RoleId
                               FROM UsersRoles;";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted)
		{
			string query = @"SELECT UserId, RoleId
                               FROM UsersRoles
                              WHERE UserId = @UserId;";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted)
		{
			string query = @"SELECT UserId, RoleId
                               FROM UsersRoles
                              WHERE UserId = @UserId
                                AND RoleId = @RoleId;";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		#endregion

		#region Add

		protected override void ConfigureCommandForInsert(IDbCommand command)
		{
			string query = @"INSERT INTO UsersRoles (UserId, RoleId) VALUES ( @UserId, @RoleId );";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		#endregion

		#region Update

		protected override void ConfigureCommandForUpdate(IDbCommand command)
		{
			string query = @"UPDATE UsersRoles SET
                                     UserId = @UserId,
                                     RoleId = @RoleId
                              WHERE UserId = @UserId
                                AND RoleId = @RoleId;";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		#endregion

		#region Delete

		protected override void ConfigureCommandForDelete(IDbCommand command)
		{
			string query = "";

			query = @"DELETE FROM UsersRoles
                            WHERE UserId = @UserId
                              AND RoleId = @RoleId;";

			command.CommandText = query;
			command.CommandType = CommandType.Text;
		}

		#endregion

		#endregion

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

		#endregion

	}

	public class UserRoleRepository<TUserRole> : UserRoleRepository<int, TUserRole>, IUserRoleRepository<TUserRole>
		where TUserRole : CoreUserRole, new()
	{
		public UserRoleRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}
	}
}
