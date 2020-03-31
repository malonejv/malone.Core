using malone.Core.DAL.AdoNet.Factory;
using malone.Core.DAL.AdoNet.Provider;
using malone.Core.DAL.Base.Context;
using System;
using System.Data;

namespace malone.Core.DAL.AdoNet.Context
{
    public abstract class AdoNetContext : IDisposable, IContext
    {
        private bool isDisposed;

        public IDbConnection Connection { get; private set; }
        protected IDbTransaction _transaction;

        public DatabaseFactory DbFactory { get; set; }

        private IDatabase _db;
        public IDatabase Db
        {
            get
            {
                if (_db == null)
                {
                    _db = DbFactory.CreateDatabase();
                }

                return _db;
            }
        }


        public AdoNetContext(DatabaseFactory databaseFactory)
        {
            DbFactory = databaseFactory;
            Connection = Db.CreateConnection();
            Connection.Open();
            _transaction = Connection.BeginTransaction();
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                    Connection = null;
                }
            }

            isDisposed = true;
        }
    }
}
