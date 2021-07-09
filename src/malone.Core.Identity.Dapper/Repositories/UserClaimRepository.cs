using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class UserClaimRepository<TKey, TUserClaim> : Repository<TKey, TUserClaim>, IUserClaimRepository<TKey, TUserClaim>
         where TKey : IEquatable<TKey>
         where TUserClaim : CoreUserClaim<TKey>, new()
    {
        public UserClaimRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        //#region Crud Operations

        //#region Get

        //protected override void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, UserId, ClaimType, ClaimValue
        //                       FROM UsersClaims
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, UserId, ClaimType, ClaimValue
        //                       FROM UsersClaims;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, UserId, ClaimType, ClaimValue
        //                       FROM UsersClaims
        //                      WHERE UserId = @UserId;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT Id, UserId, ClaimType, ClaimValue
        //                       FROM UsersClaims
        //                      WHERE UserId = @UserId
        //                        AND ClaimType = @ClaimType
        //                        AND ClaimValue = @ClaimValue;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Add

        //protected override void ConfigureCommandForInsert(IDbCommand command)
        //{
        //    string query = @"INSERT INTO UsersClaims (UserId, ClaimType, ClaimValue) VALUES ( @UserId, @ClaimType, @ClaimValue );";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Update

        //protected override void ConfigureCommandForUpdate(IDbCommand command)
        //{
        //    string query = @"UPDATE UsersClaims SET  
        //                             UserId = @UserId,
        //                             ClaimType = @ClaimType,
        //                             ClaimValue = @ClaimValue
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Delete

        //protected override void ConfigureCommandForDelete(IDbCommand command)
        //{
        //    string query = @"DELETE FROM UsersClaims 
        //                      WHERE Id = @Id;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#endregion

        protected override TUserClaim Map(DataRow row)
        {
            TUserClaim userLogin = null;
            if (!row.IsNull())
            {
                userLogin = new TUserClaim();
                userLogin.Id = row.AsTOrDefault<TKey>("Id");
                userLogin.UserId = row.AsTOrDefault<TKey>("UserId");
                userLogin.ClaimType = row.AsString("ClaimType");
                userLogin.ClaimValue = row.AsString("ClaimValue");
            }
            return userLogin;
        }

        #endregion

    }

    public class UserClaimRepository<TUserClaim> : UserClaimRepository<int, TUserClaim>, IUserClaimRepository<TUserClaim>
        where TUserClaim : CoreUserClaim, new()
    {
        public UserClaimRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
