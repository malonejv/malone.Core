using Dapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class UserClaimRepository<TKey, TUserClaim> : CustomRepository<TUserClaim>, IUserClaimRepository<TKey, TUserClaim>
         where TKey : IEquatable<TKey>
         where TUserClaim : CoreUserClaim<TKey>, new()
    {
        public UserClaimRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

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

        #region Public Methods

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            string commandText = "Select * from UsersClaims where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@UserId", userId } };

            var rows = Connection.Query<TUserClaim>(commandText, parameters, transaction: Context.Transaction);

            foreach (var row in rows)
            {
                Claim claim = new Claim(row.ClaimType, row.ClaimValue);
                claims.AddClaim(claim);
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Delete from UsersClaims where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("userId", userId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public int Insert<TUserKey>(TUserKey userId, Claim userClaim)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Insert into UsersClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("value", userClaim.Value);
            parameters.Add("type", userClaim.Type);
            parameters.Add("userId", userId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public int Delete<TUserKey>(TUserKey userId, Claim claim)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Delete from UsersClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("userId", userId);
            parameters.Add("value", claim.Value);
            parameters.Add("type", claim.Type);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
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
