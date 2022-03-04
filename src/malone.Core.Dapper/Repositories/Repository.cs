using System;
using System.Data;
using System.Linq;
using Dapper;
using malone.Core.Commons.Exceptions;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Dapper.Repositories
	{
	public abstract class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        //protected AdoNetDbContext Context { get; private set; }
        //protected ICoreLogger Logger { get; }


        public Repository(IContext context, ICoreLogger logger) : base(context, logger) { }

        #region CRUD Operations

        #region GET BY ID

        protected virtual CommandDefinition ConfigureCommandForGetById(TKey id, bool includeDeleted, string includeProperties)
        {
            string tableName = typeof(TEntity).GetTableName();
            string columns = typeof(TEntity).GetColumnNames();
            string query = string.Format("SELECT {0} FROM {1}", columns, tableName);

            DynamicParameters parameters = new DynamicParameters();
            var allowSoftDelete = ConfigureParameterIsDelete(query, columns, tableName);

            var whereClause = "";
            ParameterAttribute parametersInfo;

            var IdColumnAttribute = typeof(TEntity).GetKeyColumnInfo();
            IdColumnAttribute.Value = id;

            int? size = IdColumnAttribute.IsSizeDefined ? new int?(IdColumnAttribute.Size) : null;
            parameters.Add(IdColumnAttribute.Name, IdColumnAttribute.Value, IdColumnAttribute.Type, IdColumnAttribute.Direction, size);

            if (allowSoftDelete)
            {
                whereClause = " AND ";
            }
            else
            {
                whereClause = " WHERE ";
            }

            int i = 0;
            var ColumnNames = columns.Split(',');
            foreach (var parameterName in parameters.ParameterNames)
            {
                var exists = ColumnNames.Contains(parameterName);
                if (exists)
                {
                    if (i == 0)
                    {
                        whereClause += $"@{parameterName} = {parameterName}";
                    }
                    else
                    {
                        whereClause += $" AND @{parameterName} = {parameterName}";
                    }
                }

                i++;
            }
            query += $"{whereClause};";

            return new CommandDefinition(query, transaction: Context.Transaction, commandType: CommandType.Text, parameters: parameters);
        }

        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            ThrowIfDisposed();
            try
            {
                IQueryable<TEntity> query;

                var command = ConfigureCommandForGetById(id, includeDeleted, includeProperties);
                query = GetQueryable(command);

                return query.SingleOrDefault<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region UPDATE

        //protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity entity)
        //{
        //    DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
        //    parameters.Add(parameter);

        //    parameters.AddRange(entity.GetNotKeyParameters<TKey, TEntity>().ToList());

        //    return parameters;
        //}

        public virtual void Update(TEntity entity)
        {
            //ThrowIfDisposed();
            //try
            //{
            //    var command = Context.CreateCommand();
            //    ConfigureCommandForUpdate(command);

            //    List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
            //    parameters = GetUpdateParameters(parameters, entity);
            //    Context.AddCommandParameters(command, parameters);

            //    command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
            //    if (Logger != null) Logger.Error(techEx);

            //    throw techEx;
            //}
        }

        #endregion

        #region DELETE

        //protected override List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
        //{
        //    DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
        //    parameters.Add(parameter);
        //    return parameters;
        //}

        #endregion

        #endregion

        #region Private And Protected Methods

        #endregion

    }


    public abstract class Repository<TEntity> : Repository<int, TEntity>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IContext context, ICoreLogger logger) : base(context, logger)
        {
        }
    }
}
