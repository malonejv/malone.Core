using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.Logging;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.Dapper.Repositories
	{
	public class UserLoginRepository<TKey, TUserLogin> : CustomRepository<TUserLogin>, IUserLoginRepository<TKey, TUserLogin>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
    {
        public UserLoginRepository(IContext context, ICoreLogger logger) : base(context, logger)
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

        public int Delete<TUserKey>(TUserKey userId)
        {
            string commandText = "Delete from UsersLogins where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("UserId", userId);

            return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
        }

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

        public TUserKey FindUserIdByLogin<TUserKey>(UserLoginInfo userLogin)
where TUserKey : IEquatable<TUserKey>
        {
            string commandText = "Select UserId from UsersLogins where LoginProvider = @loginProvider and ProviderKey = @providerKey";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("loginProvider", userLogin.LoginProvider);
            parameters.Add("providerKey", userLogin.ProviderKey);

            return Connection.QueryFirstOrDefault<TUserKey>(commandText, parameters, transaction: Context.Transaction);
        }

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
        public UserLoginRepository(IContext context, ICoreLogger logger) : base(context, logger)
        {
        }
    }
}