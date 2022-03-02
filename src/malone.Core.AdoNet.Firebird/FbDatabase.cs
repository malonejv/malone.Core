namespace malone.Core.AdoNet.Firebird
{
	using System;
	using System.Data;
	using FirebirdSql.Data.FirebirdClient;
	using malone.Core.AdoNet.Database;

	/// <summary>
	/// Defines the <see cref="T: FbDatabase" />.
	/// </summary>
	public class FbDatabase : IDatabase
	{
		/// <summary>
		/// Gets or sets the ConnectionString.
		/// </summary>
		private string ConnectionString { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T: FbDatabase"/> class.
		/// </summary>
		/// <param name="connectionString">The connectionString<see cref="T: string"/>.</param>
		public FbDatabase(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// The CreateConnection.
		/// </summary>
		/// <returns>The <see cref="T: IDbConnection"/>.</returns>
		public IDbConnection CreateConnection()
		{
			return new FbConnection(ConnectionString);
		}

		/// <summary>
		/// The CloseConnection.
		/// </summary>
		/// <param name="connection">The connection<see cref="T: IDbConnection"/>.</param>
		public void CloseConnection(IDbConnection connection)
		{
			var fbconnection = (FbConnection)connection;
			fbconnection.Close();
			fbconnection.Dispose();
		}

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDataAdapter"/>.</returns>
		public IDataAdapter CreateAdapter(IDbCommand command)
		{
			return new FbDataAdapter((FbCommand)command);
		}

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDbDataParameter"/>.</returns>
		public IDbDataParameter CreateParameter(IDbCommand command)
		{
			FbCommand fbCommand = (FbCommand)command;
			return fbCommand.CreateParameter();
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
			FbCommand fbCommand = FbDatabase.ValidateCommand(command);
			if (parameterType is FbDbType)
			{
				var dbType = (FbDbType)parameterType;
				FbParameter fbParameter = fbCommand.Parameters.Add(parameterName, dbType);
				fbParameter.Value = value;
				fbParameter.Direction = parameterdirection;
			}
			else
			{
				//TODO: manejar con errores del core.
				throw new InvalidOperationException(string.Format("FbType unrecognized: {0}", parameterName));
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
			FbCommand fbCommand = FbDatabase.ValidateCommand(command);
			if (parameterType is FbDbType)
			{
				var dbType = (FbDbType)parameterType;
				FbParameter fbParameter = fbCommand.Parameters.Add(parameterName, dbType, size);
				fbParameter.Value = value;
				fbParameter.Direction = parameterdirection;
			}
			else
			{
				//TODO: manejar con errores del core.
				throw new InvalidOperationException(string.Format("FbType unrecognized: {0}", parameterName));
			}
		}

		/// <summary>
		/// The ValidateCommand.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: FbCommand"/>.</returns>
		private static FbCommand ValidateCommand(IDbCommand command)
		{
			if (!(command is FbCommand fbCommand))
			{
				throw new InvalidOperationException("Error");
			}
			//TODO: manejar con errores del core.
			//string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.FbCommandExpected, (object)command.GetType().FullName))
			return fbCommand;
		}
	}
}
