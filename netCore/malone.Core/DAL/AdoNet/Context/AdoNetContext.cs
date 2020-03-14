using malone.Core.DAL.Base.Context;
using System;
using System.Data;

namespace malone.Core.DAL.AdoNet.Context
{
    public abstract class AdoNetContext : IDisposable, IContext
    {
        //private IDbConnection _connection;
        //private IDbTransaction _transaction;
        private string _connectionStringName = null;

        public IDbConnection Connection { get; private set; }
        protected IDbTransaction _transaction;

        private Database _db;
        public Database Db
        {
            get
            {
                if (_db == null)
                {
                    _db = DatabaseFactory.CreateDatabase(_connectionStringName);
                }

                return _db;
            }
        }


        public AdoNetContext(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            Connection = Db.CreateConnection();
            Connection.Open();
            _transaction = Connection.BeginTransaction(); ;
        }
        public IDbCommand CreateCommand()
        {
            var command = Connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }
        public int SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");
            }
            _transaction.Commit();
            _transaction = null;

            return 0;
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
        }
    }
}
