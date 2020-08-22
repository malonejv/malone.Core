using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using malone.Core.AdoNet.Database;
using malone.Core.Commons.DI;
using malone.Core.DataAccess.Context;

namespace malone.Core.AdoNet.Context
{
    public class AdoNetDbContext : IDisposable, IContext
    {
        private IDatabase _db;
        private bool _isDisposed;

        protected string ConnectionStringName { get; private set; }

        protected DatabaseFactory DbFactory { get; private set; }

        protected IDbConnection Connection { get; private set; }
        
        protected IDbTransaction Transaction { get; private set; }

        protected IDatabase Db
        {
            get
            {
                if (_db == null)
                {
                    _db = DbFactory.CreateDatabase(ConnectionStringName);
                }

                return _db;
            }
        }


        public AdoNetDbContext(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            DbFactory = ServiceLocator.Current.Get<DatabaseFactory>();
            Connection = Db.CreateConnection();
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand command = Connection.CreateCommand(); 
            command.Transaction = Transaction;

            return command;

            //var command = Db.CreateCommand(commandText, commandType, Connection);
            //command.Transaction = Transaction;
            //return command;
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return Db.CreateAdapter(command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            return command.CreateParameter();
        }

        public void AddCommandParameters(IDbCommand command, IEnumerable<DbParameterWithValue> parameters)
        {
            foreach (DbParameterWithValue parameter in parameters.OrderBy<DbParameterWithValue, int>(e => e.DbParameter.Order))
            {
                    if (parameter.DbParameter.IsSizeDefined)
                        Db.AddCommandParameter(command, parameter.DbParameter.Name, parameter.Value, parameter.DbParameter.Direction, parameter.DbParameter.Type, parameter.DbParameter.Size);
                    else
                        Db.AddCommandParameter(command, parameter.DbParameter.Name, parameter.Value, parameter.DbParameter.Direction, parameter.DbParameter.Type);
            }
        }

        public int SaveChanges()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");
            }
            Transaction.Commit();
            Transaction = null;

            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                if (Transaction != null)
                {
                    Transaction.Rollback();
                    Transaction.Dispose();
                    Transaction = null;
                }
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                    Connection = null;
                    DbFactory = null;
                }
            }

            _isDisposed = true;
        }
    }
}
