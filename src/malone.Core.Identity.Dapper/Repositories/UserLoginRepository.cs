﻿using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Data;

namespace malone.Core.Identity.Dapper.Repositories
{
    public class UserLoginRepository<TKey, TUserLogin> : BaseRepository<TUserLogin>, IUserLoginRepository<TKey, TUserLogin>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
    {
        public UserLoginRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        //#region Crud Operations

        //#region Get

        //protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT LoginProvider, ProviderKey, UserId
        //                       FROM UsersLogins;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT LoginProvider, ProviderKey, UserId
        //                       FROM UsersLogins
        //                      WHERE UserId = @UserId;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        //{
        //    string query = @"SELECT LoginProvider, ProviderKey, UserId
        //                       FROM UsersLogins
        //                      WHERE LoginProvider = @LoginProvider
        //                        AND ProviderKey = @ProviderKey
        //                        AND UserId = @UserId;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Add

        //protected override void ConfigureCommandForInsert(IDbCommand command)
        //{
        //    string query = @"INSERT INTO UsersLogins (LoginProvider, ProviderKey, UserId) VALUES ( @LoginProvider, @ProviderKey, @UserId );";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Update

        //protected override void ConfigureCommandForUpdate(IDbCommand command)
        //{
        //    string query = @"UPDATE UsersLogins SET 
        //                             LoginProvider = @LoginProvider,
        //                             ProviderKey = @ProviderKey
        //                      WHERE LoginProvider = @LoginProvider
        //                        AND ProviderKey = @ProviderKey 
        //                        AND UserId = @UserId;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        //#region Delete

        //protected override void ConfigureCommandForDelete(IDbCommand command)
        //{
        //    string query = "";

        //    query = @"DELETE FROM UsersLogins 
        //                    WHERE LoginProvider = @LoginProvider
        //                      AND ProviderKey = @ProviderKey
        //                      AND UserId = @UserId;";

        //    command.CommandText = query;
        //    command.CommandType = CommandType.Text;
        //}

        //#endregion

        #endregion

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

        //#endregion

    }

    public class UserLoginRepository<TUserLogin> : UserLoginRepository<int, TUserLogin>, IUserLoginRepository<TUserLogin>
        where TUserLogin : CoreUserLogin, new()
    {
        public UserLoginRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}