namespace malone.Core.AdoNet.Oracle.Attributes
{
	using System;
	using System.Data;
	using malone.Core.AdoNet.Attributes;

	/// <summary>
	/// Defines the <see cref="T: OracleResultSetAttribute" />.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class OracleResultSetAttribute : DbParameterAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T: OracleResultSetAttribute"/> class.
		/// </summary>
		/// <param name="name">The name<see cref="T: string"/>.</param>
		public OracleResultSetAttribute(string name) : base(name)
		{
			base.Direction = ParameterDirection.Output;
		}
	}
}
