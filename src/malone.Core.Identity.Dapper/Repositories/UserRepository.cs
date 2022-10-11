using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Dapper.Repositories;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.Logging;

namespace malone.Core.Identity.Dapper.Repositories
{
	public class UserRepository<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : CustomRepository<TUserEntity>, IUserRepository<TKey, TUserEntity>
		 where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>
		where TUserRole : CoreUserRole<TKey>
		where TUserClaim : CoreUserClaim<TKey>
		 where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
	{
		public UserRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}

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

		#region Public Methods

		public string GetUserName(TKey userId)
		{
			string commandText = "Select Name from Users where Id = @id";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

			return Connection.QueryFirstOrDefault<string>(commandText, parameters, transaction: Context.Transaction);
		}

		public string GetUserId(string userName)
		{
			string commandText = "Select Id from Users where UserName = @name";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

			return Connection.QueryFirstOrDefault<string>(commandText, parameters, transaction: Context.Transaction);
		}

		public TUserEntity GetUserById(TKey userId)
		{
			TUserEntity user = null;
			string commandText = "Select * from Users where Id = @id";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

			//var rows = Connection.Query<TUserEntity>(commandText, parameters, transaction: Context.Transaction);
			//if (rows != null && rows.Count == 1)
			//{
			//    var row = rows[0];
			//    user = (TUserEntity)Activator.CreateInstance(typeof(TUserEntity));
			//    user.Id = row["Id"];
			//    user.UserName = row["UserName"];
			//    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
			//    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
			//    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
			//    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
			//    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
			//    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
			//    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
			//    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
			//    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
			//}

			//return user;
			return Connection.QueryFirstOrDefault<TUserEntity>(commandText, parameters, transaction: Context.Transaction);
		}

		public List<TUserEntity> GetUserByName(string userName)
		{
			List<TUserEntity> users = new List<TUserEntity>();
			string commandText = "Select * from Users where UserName = @name";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

			var rows = Connection.Query<TUserEntity>(commandText, parameters, transaction: Context.Transaction);

			//foreach (var row in rows)
			//{
			//    TUserEntity user = (TUserEntity)Activator.CreateInstance(typeof(TUserEntity));
			//    user.Id = row["Id"];
			//    user.UserName = row["UserName"];
			//    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
			//    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
			//    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
			//    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
			//    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
			//    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
			//    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
			//    user.TwoFactorEnabled = row["TwoFactorEnabled"] == "1" ? true : false;
			//    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
			//    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
			//    users.Add(user);
			//}

			return rows.ToList();
		}

		public List<TUserEntity> GetUserByEmail(string email)
		{
			List<TUserEntity> users = new List<TUserEntity>();
			string commandText = "Select * from Users where Email = @email";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@email", email } };

			var rows = Connection.Query<TUserEntity>(commandText, parameters, transaction: Context.Transaction);
			//foreach (var row in rows)
			//{
			//    TUserEntity user = (TUserEntity)Activator.CreateInstance(typeof(TUserEntity));
			//    user.Id = row["Id"];
			//    user.UserName = row["UserName"];
			//    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
			//    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
			//    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
			//    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
			//    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
			//    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
			//    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
			//    user.TwoFactorEnabled = row["TwoFactorEnabled"] == "1" ? true : false;
			//    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
			//    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
			//    users.Add(user);
			//}

			return rows.ToList();
		}

		public string GetPasswordHash(TKey userId)
		{
			string commandText = "Select PasswordHash from Users where Id = @id";
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("@id", userId);

			var passHash = Connection.QueryFirstOrDefault<string>(commandText, parameters, transaction: Context.Transaction);
			if (string.IsNullOrEmpty(passHash))
			{
				return null;
			}

			return passHash;
		}

		public int SetPasswordHash(TKey userId, string passwordHash)
		{
			string commandText = "Update Users set PasswordHash = @pwdHash where Id = @id";
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("@pwdHash", passwordHash);
			parameters.Add("@id", userId);

			return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
		}

		public string GetSecurityStamp(TKey userId)
		{
			string commandText = "Select SecurityStamp from Users where Id = @id";
			Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };
			var result = Connection.QueryFirstOrDefault<string>(commandText, parameters, transaction: Context.Transaction);

			return result;
		}

		public int Insert(TUserEntity user)
		{
			string commandText = @"Insert into Users (UserName, Id, PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
                values (@name, @id, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)";
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("@name", user.UserName);
			parameters.Add("@id", user.Id);
			parameters.Add("@pwdHash", user.PasswordHash);
			parameters.Add("@SecStamp", user.SecurityStamp);
			parameters.Add("@email", user.Email);
			parameters.Add("@emailconfirmed", user.EmailConfirmed);
			parameters.Add("@phonenumber", user.PhoneNumber);
			parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
			parameters.Add("@accesscount", user.AccessFailedCount);
			parameters.Add("@lockoutenabled", user.LockoutEnabled);
			parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
			parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

			return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
		}

		private int Delete(TKey userId)
		{
			string commandText = "Delete from Users where Id = @userId";
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("@userId", userId);

			return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
		}

		public int Delete(TUserEntity user)
		{
			return Delete(user.Id);
		}

		public int Update(TUserEntity user)
		{
			string commandText = @"Update Users set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp,
                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled
                WHERE Id = @userId";
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("@userName", user.UserName);
			parameters.Add("@pswHash", user.PasswordHash);
			parameters.Add("@secStamp", user.SecurityStamp);
			parameters.Add("@userId", user.Id);
			parameters.Add("@email", user.Email);
			parameters.Add("@emailconfirmed", user.EmailConfirmed);
			parameters.Add("@phonenumber", user.PhoneNumber);
			parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
			parameters.Add("@accesscount", user.AccessFailedCount);
			parameters.Add("@lockoutenabled", user.LockoutEnabled);
			parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
			parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

			return Connection.Execute(commandText, parameters, transaction: Context.Transaction);
		}

		#endregion


	}

	public class UserRepository<TUserEntity, TUserLogin, TUserRole, TUserClaim> : UserRepository<int, TUserEntity, TUserLogin, TUserRole, TUserClaim>, IUserRepository<TUserEntity>
		where TUserLogin : CoreUserLogin
		where TUserRole : CoreUserRole
		where TUserClaim : CoreUserClaim
		 where TUserEntity : CoreUser<TUserLogin, TUserRole, TUserClaim>, new()
	{
		public UserRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}
	}
}
