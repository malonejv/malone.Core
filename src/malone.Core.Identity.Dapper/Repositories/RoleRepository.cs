using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class RoleRepository<TKey, TRoleEntity, TUserRole> : Repository<TKey, TRoleEntity>, IRoleRepository<TKey, TRoleEntity>
         where TKey : IEquatable<TKey>
         where TUserRole : CoreUserRole<TKey>, new()
         where TRoleEntity : CoreRole<TKey, TUserRole>, new()
    {
        public RoleRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        //#region Crud Operations

        //#region Get

        //public override TRoleEntity GetById(TKey id, bool includeDeleted = false, string includeProperties = "Users")
        //{
        //    TRoleEntity role = base.GetById(id, includeDeleted, includeProperties);

        //    role.Users = new List<TUserRole>();

        //    return role;
        //}

        //protected override void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, Name
        //                       FROM Roles
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //public override IEnumerable<TRoleEntity> GetAll(Func<IQueryable<TRoleEntity>, IOrderedQueryable<TRoleEntity>> orderBy = null, bool includeDeleted = false, string includeProperties = "Users")
        //{
        //    IEnumerable<TRoleEntity> roles = base.GetAll(orderBy, includeDeleted, includeProperties);

        //    foreach (var role in roles)
        //    {
        //        role.Users = new List<TUserRole>();
        //    }
        //    return roles;
        //}
        //protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, Name
        //                       FROM Roles;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //public override IEnumerable<TRoleEntity> Get<TFilter>(TFilter filter = null, Func<IQueryable<TRoleEntity>, IOrderedQueryable<TRoleEntity>> orderBy = null, bool includeDeleted = false, string includeProperties = "Users")
        //{
        //    IEnumerable<TRoleEntity> roles = base.Get(filter, orderBy, includeDeleted, includeProperties);

        //    foreach (var role in roles)
        //    {
        //        role.Users = new List<TUserRole>();
        //    }
        //    return roles;
        //}
        //protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, Name
        //                       FROM Roles
        //                      WHERE UPPER(Name) = UPPER(@Name);";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //public override TRoleEntity GetEntity<TFilter>(TFilter filter = null, Func<IQueryable<TRoleEntity>, IOrderedQueryable<TRoleEntity>> orderBy = null, bool includeDeleted = false, string includeProperties = "Users")
        //{
        //    TRoleEntity role = base.GetEntity(filter, orderBy, includeDeleted, includeProperties);

        //    role.Users = new List<TUserRole>();

        //    return role;
        //}
        //protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, Name
        //                       FROM Roles
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Add

        //protected override void ConfigureCommandForInsert(IDbCommand command)
        //{
        //    string query = @"INSERT INTO Roles (Name) VALUES ( @Name );";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Update

        //protected override void ConfigureCommandForUpdate(IDbCommand command)
        //{
        //    string query = @"UPDATE Roles SET  
        //                             Name = @Name,
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Delete

        //protected override void ConfigureCommandForDelete(IDbCommand command)
        //{
        //    string query = @"DELETE FROM Roles 
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#endregion

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
