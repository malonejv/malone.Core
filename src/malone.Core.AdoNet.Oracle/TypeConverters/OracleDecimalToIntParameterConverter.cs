using Oracle.ManagedDataAccess.Types;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="OracleDecimalToIntParameterConverter" />.
	/// </summary>
	public class OracleDecimalToIntParameterConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <returns>The <see cref="object"/>.</returns>
		public object Convert(object value)
		{
			return ((OracleDecimal)value).ToInt32();
		}
	}
}
