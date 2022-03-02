using Oracle.ManagedDataAccess.Client;

namespace malone.Core.AdoNet.Oracle
{
	using System;
	using System.Data;
	using malone.Core.AdoNet.Database;

	/// <summary>
	/// Defines the <see cref="OracleDatabase" />.
	/// </summary>
	public class OracleDatabase : IDatabase
	{
		/// <summary>
		/// Gets or sets the ConnectionString.
		/// </summary>
		private string ConnectionString { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="OracleDatabase"/> class.
		/// </summary>
		/// <param name="connectionString">The connectionString<see cref="string"/>.</param>
		public OracleDatabase(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// The CreateConnection.
		/// </summary>
		/// <returns>The <see cref="IDbConnection"/>.</returns>
		public IDbConnection CreateConnection()
		{
			return new OracleConnection(ConnectionString);
		}

		/// <summary>
		/// The CloseConnection.
		/// </summary>
		/// <param name="connection">The connection<see cref="IDbConnection"/>.</param>
		public void CloseConnection(IDbConnection connection)
		{
			var oracleconnection = (OracleConnection)connection;
			oracleconnection.Close();
			oracleconnection.Dispose();
		}

		/// <summary>
		/// The CreateCommand.
		/// </summary>
		/// <param name="commandText">The commandText<see cref="string"/>.</param>
		/// <param name="commandType">The commandType<see cref="CommandType"/>.</param>
		/// <param name="connection">The connection<see cref="IDbConnection"/>.</param>
		/// <returns>The <see cref="IDbCommand"/>.</returns>
		public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
		{
			return new OracleCommand()
			{
				CommandText = commandText,
				CommandType = commandType,
				Connection = (OracleConnection)connection
			};
		}

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <returns>The <see cref="IDataAdapter"/>.</returns>
		public IDataAdapter CreateAdapter(IDbCommand command)
		{
			return new OracleDataAdapter((OracleCommand)command);
		}

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <returns>The <see cref="IDbDataParameter"/>.</returns>
		public IDbDataParameter CreateParameter(IDbCommand command)
		{
			OracleCommand oracleCommand = (OracleCommand)command;

			return oracleCommand.CreateParameter();
		}

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="string"/>.</param>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="object"/>.</param>
		public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="string"/>.</param>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="object"/>.</param>
		/// <param name="size">The size<see cref="int"/>.</param>
		public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size)
		{
			throw new NotImplementedException();
		}
	}
}
