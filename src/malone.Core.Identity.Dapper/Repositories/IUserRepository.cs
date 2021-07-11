using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IUserRepository<TKey, TEntity> : ICustomRepository<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        string GetUserName(TKey userId);
        string GetUserId(string userName);
        TEntity GetUserById(TKey userId);
        List<TEntity> GetUserByName(string userName);
        List<TEntity> GetUserByEmail(string email);
        string GetPasswordHash(TKey userId);
        int SetPasswordHash(TKey userId, string passwordHash);
        string GetSecurityStamp(TKey userId);
        int Insert(TEntity user);
        int Delete(TEntity user);
        int Update(TEntity user);
    }

    public interface IUserRepository<TEntity> : IUserRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
