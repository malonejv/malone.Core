using malone.Core.Commons.DI;
using malone.Core.Dapper.Database;
using malone.Core.DataAccess.Context;
using System;
using System.Data;

namespace malone.Core.Dapper.Context
{
    public class CoreDbContext : IDisposable, IContext
    {
        private IDatabase _db;
        private bool _isDisposed;

        protected string ConnectionStringName { get; private set; }

        protected DatabaseFactory DbFactory { get; private set; }

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

        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public CoreDbContext(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            DbFactory = ServiceLocator.Current.Get<DatabaseFactory>();
            Connection = Db.CreateConnection();
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public int SaveChanges()
        {
            if (Transaction == null)
            {
                //TODO: Usar errores Core
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
