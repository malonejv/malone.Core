namespace malone.Core.AdoNet.Database
{
	using System.Data;

	/// <summary>
	/// Defines the <see cref="T: IDatabase" />.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		/// The CreateConnection.
		/// </summary>
		/// <returns>The <see cref="T: IDbConnection"/>.</returns>
		IDbConnection CreateConnection();

		/// <summary>
		/// The CloseConnection.
		/// </summary>
		/// <param name="connection">The connection<see cref="T: IDbConnection"/>.</param>
		void CloseConnection(IDbConnection connection);

		/// <summary>
		/// The CreateAdapter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDataAdapter"/>.</returns>
		IDataAdapter CreateAdapter(IDbCommand command);

		/// <summary>
		/// The CreateParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <returns>The <see cref="T: IDbDataParameter"/>.</returns>
		IDbDataParameter CreateParameter(IDbCommand command);

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="T: string"/>.</param>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="T: ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="T: object"/>.</param>
		void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType);

		/// <summary>
		/// The AddCommandParameter.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="parameterName">The parameterName<see cref="T: string"/>.</param>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <param name="parameterdirection">The parameterdirection<see cref="T: ParameterDirection"/>.</param>
		/// <param name="parameterType">The parameterType<see cref="T: object"/>.</param>
		/// <param name="size">The size<see cref="T: int"/>.</param>
		void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size);
	}
}
