using Oracle.ManagedDataAccess.Types;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="T: OracleDecimalToIntParameterConverter" />.
	/// </summary>
	public class OracleDecimalToIntParameterConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public object Convert(object value)
		{
			return ((OracleDecimal)value).ToInt32();
		}
	}
}
