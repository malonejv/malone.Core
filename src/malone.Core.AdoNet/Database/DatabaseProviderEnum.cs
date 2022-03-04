namespace malone.Core.AdoNet.Database
{
	using System.ComponentModel;

	/// <summary>
	/// Defines the DatabaseProvider.
	/// </summary>
	public enum DatabaseProvider
	{
		/// <summary>
		/// Defines the SqlServer.
		/// </summary>
		[Description("malone.Core.AdoNet.SqlServer.SqlDatabase, malone.Core.AdoNet.SqlServer")]
		SqlServer,

		/// <summary>
		/// Defines the Oracle.
		/// </summary>
		[Description("malone.Core.AdoNet.Oracle.OracleDatabase, malone.Core.AdoNet.Oracle")]
		Oracle,

		/// <summary>
		/// Defines the Firebird.
		/// </summary>
		[Description("malone.Core.AdoNet.Firebird.FbDatabase, malone.Core.AdoNet.Firebird")]
		Firebird
	}
}
