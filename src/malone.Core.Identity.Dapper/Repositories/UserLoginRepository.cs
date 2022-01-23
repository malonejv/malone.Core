using Dapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class UserLoginRepository<TKey, TUserLogin> : CustomRepository<TUserLogin>, IUserLoginRepository<TKey, TUserLogin>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
    {
        public UserLoginRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        protected override TUserLogin Map(DataRow row)
        {
            TUserLogin userLogin = null;
            if (!row.IsNull())
            {
                userLogin = new TUserLogin();
                userLogin.LoginProvider = row.AsString("LoginProvider");
                userLogin.ProviderKey = row.AsString("ProviderKey");
                userLogin.UserId = row.AsTOrDefault<TKey>("UserId");
            }
            return userLogin;
        }

        #region Public Methods

        /// <summary>
        /// Deletes a login from a user in the UsersLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public int Delete<TUserKey>(TUserKey userId, UserLoginInfo login)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Delete from UsersLogins where UserId = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("UserId", userId);
            parameters.Add("loginProvider", login.LoginProvider);
            parameters.Add("providerKey", login.ProviderKey);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Deletes all Logins from a user in the UsersLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete<TUserKey>(TUserKey userId)
        {
            string commandText = "Delete from UsersLogins where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("UserId", userId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Inserts a new login in the UsersLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public int Insert<TUserKey>(TUserKey userId, UserLoginInfo login)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Insert into UsersLogins (LoginProvider, ProviderKey, UserId) values (@loginProvider, @providerKey, @userId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("loginProvider", login.LoginProvider);
            parameters.Add("providerKey", login.ProviderKey);
            parameters.Add("userId", userId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns></returns>
        public TUserKey FindUserIdByLogin<TUserKey>(UserLoginInfo userLogin)
            where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Select UserId from UsersLogins where LoginProvider = @loginProvider and ProviderKey = @providerKey";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("loginProvider", userLogin.LoginProvider);
            parameters.Add("providerKey", userLogin.ProviderKey);

            return Connection.QueryFirstOrDefault<TUserKey>(commandText, parameters, transaction: Context.Transaction);
        }

        /// <summary>
        /// Returns a list of user's logins
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            string commandText = "Select * from UsersLogins where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@userId", userId } };

            var rows = Connection.Query<UserLoginInfo>(commandText, parameters, transaction: Context.Transaction);
            foreach (var row in rows)
            {
                var login = new UserLoginInfo(row.LoginProvider, row.ProviderKey);
                logins.Add(login);
            }

            return logins;
        }

        #endregion


    }

    public class UserLoginRepository<TUserLogin> : UserLoginRepository<int, TUserLogin>, IUserLoginRepository<TUserLogin>
        where TUserLogin : CoreUserLogin, new()
    {
        public UserLoginRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}