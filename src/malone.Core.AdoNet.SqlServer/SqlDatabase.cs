namespace malone.Core.AdoNet.SqlServer
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using malone.Core.AdoNet.Database;

	/// <summary>
	/// Defines the <see cref="T: SqlDatabase" />.
	/// </summary>
	public class SqlDatabase : IDatabase
	{
		/// <summary>
		/// Gets or sets the ConnectionString.
		/// </summary>
		private string ConnectionString { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T: SqlDatabase"/> class.
		/// </summary>
		/// <param name="connectionString">The connectionString<see cref="T: string"/>.</param>
		public SqlDatabase(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// The CreateConnection.
		/// </summary>
		/// <returns>The <see cref="T: IDbConnection"/>.</returns>
		public IDbConnection CreateConnection()
		{

			return new SqlConnection(ConnectionString);
		}

		/// <summary>
		/// The CloseConnection.
		/// </summary>
		/// <param name="connection">The connection<see cref="T: IDbConnection"/>.</param>
		public void CloseConnection(IDbConnection connection)
		{
			var sqlconnection = (SqlConnection)connection;
			sqlconnection.Close();
			sqlconnection.Dispose();
		}

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDataAdapter"/>.</returns>
		public IDataAdapter CreateAdapter(IDbCommand command)
		{
			return new SqlDataAdapter((SqlCommand)command);
		}

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDbDataParameter"/>.</returns>
		public IDbDataParameter CreateParameter(IDbCommand command)
		{
			SqlCommand sqlCommand = (SqlCommand)command;
			return sqlCommand.CreateParameter();
		}

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="T: string"/>.</param>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="T: ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="T: object"/>.</param>
		public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType)
		{
			SqlCommand sqlCommand = SqlDatabase.ValidateCommand(command);
			if (parameterType is SqlDbType)
			{
				var dbType = (SqlDbType)parameterType;
				SqlParameter sqlParameter = sqlCommand.Parameters.Add(parameterName, dbType);
				sqlParameter.Value = value;
				sqlParameter.Direction = parameterdirection;
			}
			else
			{
				//TODO: manejar con errores del core.
				throw new InvalidOperationException(string.Format("SqlType unrecognized: {0}", parameterName));
			}
		}

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="T: string"/>.</param>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="T: ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="T: object"/>.</param>
		/// <param name="size">The size<see cref="T: int"/>.</param>
		public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size)
		{
			SqlCommand sqlCommand = SqlDatabase.ValidateCommand(command);
			if (parameterType is SqlDbType)
			{
				var dbType = (SqlDbType)parameterType;
				SqlParameter sqlParameter = sqlCommand.Parameters.Add(parameterName, dbType, size);
				sqlParameter.Value = value;
				sqlParameter.Direction = parameterdirection;
			}
			else
			{
				//TODO: manejar con errores del core.
				throw new InvalidOperationException(string.Format("SqlType unrecognized: {0}", parameterName));
			}
		}

		/// <summary>
		/// The ValidateCommand.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: SqlCommand"/>.</returns>
		private static SqlCommand ValidateCommand(IDbCommand command)
		{
			if (!(command is SqlCommand sqlCommand))
			{
				throw new InvalidOperationException("Error");
			}
			//TODO: manejar con errores del core.
			//string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.SqlCommandExpected, (object)command.GetType().FullName))
			return sqlCommand;
		}
	}
}
