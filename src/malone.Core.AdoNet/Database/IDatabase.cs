namespace malone.Core.AdoNet.Database
{
	using System.Data;

	/// <summary>
	/// Defines the <see cref="IDatabase" />.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		/// The CreateConnection.
		/// </summary>
		/// <returns>The <see cref="IDbConnection"/>.</returns>
		IDbConnection CreateConnection();

		/// <summary>
		/// The CloseConnection.
		/// </summary>
		/// <param name="connection">The connection<see cref="IDbConnection"/>.</param>
		void CloseConnection(IDbConnection connection);

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <returns>The <see cref="IDataAdapter"/>.</returns>
		IDataAdapter CreateAdapter(IDbCommand command);

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <returns>The <see cref="IDbDataParameter"/>.</returns>
		IDbDataParameter CreateParameter(IDbCommand command);

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="string"/>.</param>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="object"/>.</param>
		void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType);

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="string"/>.</param>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="object"/>.</param>
		/// <param name="size">The size<see cref="int"/>.</param>
		void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size);
	}
}
