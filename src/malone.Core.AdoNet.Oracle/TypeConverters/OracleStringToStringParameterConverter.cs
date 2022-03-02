using Oracle.ManagedDataAccess.Types;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="T: OracleStringToStringParameterConverter" />.
	/// </summary>
	public class OracleStringToStringParameterConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public object Convert(object value)
		{
			OracleString oracleString = (OracleString)value;
			if (oracleString != null)
			{
				return oracleString.ToString();
			}
			return string.Empty;
		}
	}
}
