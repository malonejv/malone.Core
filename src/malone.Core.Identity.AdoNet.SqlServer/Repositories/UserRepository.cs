using System;
using System.Data;
using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.AdoNet.SqlServer.Entities;

namespace malone.Core.Identity.AdoNet.SqlServer.Repositories
{
    public class UserRepository<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : Repository<TKey, TUserEntity>, IUserRepository<TKey, TUserEntity>
         where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserClaim : CoreUserClaim<TKey>
         where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
    {
        public UserRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        #region Crud Operations

        #region Get

        protected override void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Email, EmailConfirmed, PaswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName
                               FROM Users
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Email, EmailConfirmed, PaswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName
                               FROM Users;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Email, EmailConfirmed, PaswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName
                               FROM Users
                              WHERE UserId = @UserId;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Email, EmailConfirmed, PaswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName
                               FROM Users
                              WHERE UserId = @UserId
                                AND ClaimType = @ClaimType
                                AND ClaimValue = @ClaimValue;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Add

        protected override void ConfigureCommandForInsert(IDbCommand command)
        {
            string query = @"INSERT INTO Users (Email, EmailConfirmed, PaswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName) VALUES ( @Email, @EmailConfirmed, @PaswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName );";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Update

        protected override void ConfigureCommandForUpdate(IDbCommand command)
        {
            string query = @"UPDATE Users SET  
                                    Email = @Email,
                                    EmailConfirmed = @EmailConfirmed,
                                    PaswordHash = @PaswordHash
                                    SecurityStamp = @SecurityStamp
                                    PhoneNumber = @PhoneNumber
                                    PhoneNumberConfirmed = @PhoneNumberConfirmed
                                    TwoFactorEnabled = @TwoFactorEnabled
                                    LockoutEndDateUtc = @LockoutEndDateUtc
                                    LockoutEnabled = @LockoutEnabled
                                    AccessFailedCount = @PaswoAccessFailedCountrdHash
                                    UserName = @UserName
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Delete

        protected override void ConfigureCommandForDelete(IDbCommand command)
        {
            string query = @"DELETE FROM Users 
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #endregion

        protected override TUserEntity Map(DataRow row)
        {
            TUserEntity userLogin = null;
            if (!row.IsNull())
            {
                userLogin = new TUserEntity();
                userLogin.Id = row.AsTOrDefault<TKey>("Id");
                userLogin.Email = row.AsString("Email");
                userLogin.EmailConfirmed = row.AsBooleanOrDefault("EmailConfirmed");
                userLogin.PasswordHash = row.AsString("PasswordHash");
                userLogin.SecurityStamp = row.AsString("SecurityStamp");
                userLogin.PhoneNumber = row.AsString("PhoneNumber");
                userLogin.PhoneNumberConfirmed = row.AsBooleanOrDefault("PhoneNumberConfirmed");
                userLogin.TwoFactorEnabled = row.AsBooleanOrDefault("TwoFactorEnabled");
                userLogin.LockoutEndDateUtc = row.AsDateOrDefault("LockoutEndDateUtc");
                userLogin.LockoutEnabled = row.AsBooleanOrDefault("LockoutEnabled");
                userLogin.AccessFailedCount = row.AsIntOrDefault("AccessFailedCount");
                userLogin.UserName = row.AsString("UserName");
            }
            return userLogin;
        }

        #endregion

    }

    public class UserRepository<TUserEntity, TUserLogin, TUserRole, TUserClaim> : UserRepository<int, TUserEntity, TUserLogin, TUserRole, TUserClaim>, IUserRepository<TUserEntity>
        where TUserLogin : CoreUserLogin
        where TUserRole : CoreUserRole
        where TUserClaim : CoreUserClaim
         where TUserEntity : CoreUser<TUserLogin, TUserRole, TUserClaim>, new()
    {
        public UserRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
