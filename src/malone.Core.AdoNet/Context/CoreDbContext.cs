namespace malone.Core.AdoNet.Context
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using malone.Core.AdoNet.Database;
	using malone.Core.Commons.Helpers.Extensions;
	using malone.Core.DataAccess.Context;
	using malone.Core.IoC;

	/// <summary>
	/// Defines the <see cref="T: CoreDbContext" />.
	/// </summary>
	public class CoreDbContext : IDisposable, IContext
	{
		/// <summary>
		/// Defines the _db.
		/// </summary>
		private IDatabase _db;

		/// <summary>
		/// Defines the _isDisposed.
		/// </summary>
		private bool _isDisposed;

		/// <summary>
		/// Gets the ConnectionStringName.
		/// </summary>
		protected string ConnectionStringName { get; private set; }

		/// <summary>
		/// Gets the DbFactory.
		/// </summary>
		protected DatabaseFactory DbFactory { get; private set; }

		/// <summary>
		/// Gets the Db.
		/// </summary>
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

		/// <summary>
		/// Gets the Connection.
		/// </summary>
		public IDbConnection Connection { get; private set; }

		/// <summary>
		/// Gets the Transaction.
		/// </summary>
		public IDbTransaction Transaction { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T: CoreDbContext"/> class.
		/// </summary>
		/// <param name="connectionStringName">The connectionStringName<see cref="T: string"/>.</param>
		public CoreDbContext(string connectionStringName)
		{
			ConnectionStringName = connectionStringName;
			DbFactory = ServiceLocator.Current.Get<DatabaseFactory>();
			Connection = Db.CreateConnection();
			Connection.Open();
			Transaction = Connection.BeginTransaction();
		}

		/// <summary>
		/// The CreateCommand.
		/// </summary>
		/// <returns>The <see cref="T: IDbCommand"/>.</returns>
		public IDbCommand CreateCommand()
		{
			IDbCommand command = Connection.CreateCommand();
			command.Transaction = Transaction;

			return command;
		}

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDataAdapter"/>.</returns>
		public IDataAdapter CreateAdapter(IDbCommand command)
		{
			return Db.CreateAdapter(command);
		}

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDbDataParameter"/>.</returns>
		public IDbDataParameter CreateParameter(IDbCommand command)
		{
			return command.CreateParameter();
		}

		/// <summary>
		/// The AddCommandParameters.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="parameters">The parameters<see cref="T: IEnumerable{DbParameterWithValue}"/>.</param>
		public void AddCommandParameters(IDbCommand command, IEnumerable<DbParameterWithValue> parameters)
		{
			foreach (DbParameterWithValue parameter in parameters.OrderBy<DbParameterWithValue, int>(e => e.DbParameter.Order))
			{
				parameter.ThrowIfNull("DbParameter");

				if (parameter.DbParameter.IsSizeDefined)
				{
					Db.AddCommandParameter(command, parameter.DbParameter.Name, parameter.Value, parameter.DbParameter.Direction, parameter.DbParameter.Type, parameter.DbParameter.Size);
				}
				else
				{
					Db.AddCommandParameter(command, parameter.DbParameter.Name, parameter.Value, parameter.DbParameter.Direction, parameter.DbParameter.Type);
				}
			}
		}

		/// <summary>
		/// The SaveChanges.
		/// </summary>
		/// <returns>The <see cref="T: int"/>.</returns>
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

		/// <summary>
		/// The Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The Dispose.
		/// </summary>
		/// <param name="disposing">The disposing<see cref="T: bool"/>.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (_isDisposed)
			{
				return;
			}

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
