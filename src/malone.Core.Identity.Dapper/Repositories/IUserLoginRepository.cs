using System;
using System.Collections.Generic;
using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.Dapper.Repositories
{
	public interface IUserLoginRepository<TKey, TUserLogin> : ICustomRepository<TUserLogin>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>, new()
	{
		int Delete<TUserKey>(TUserKey userId, UserLoginInfo login)
			where TUserKey : IEquatable<TUserKey>;
		int Delete<TUserKey>(TUserKey userId);
		int Insert<TUserKey>(TUserKey userId, UserLoginInfo login)
			where TUserKey : IEquatable<TUserKey>;
		TUserKey FindUserIdByLogin<TUserKey>(UserLoginInfo userLogin)
			where TUserKey : IEquatable<TUserKey>;
		List<UserLoginInfo> FindByUserId<TUserKey>(TUserKey userId)
			where TUserKey : IEquatable<TUserKey>;

	}

	public interface IUserLoginRepository<TUserLogin> : IUserLoginRepository<int, TUserLogin>
		where TUserLogin : CoreUserLogin<int>, new()
	{
	}
}
