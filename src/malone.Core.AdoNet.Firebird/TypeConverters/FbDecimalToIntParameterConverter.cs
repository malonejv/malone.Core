namespace malone.Core.AdoNet.Oracle.Parameters
{
	using FirebirdSql.Data.FirebirdClient;
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="T: FbDecimalToIntParameterConverter" />.
	/// </summary>
	public class FbDecimalToIntParameterConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public object Convert(object value)
		{
			var fbType = ((FbDbType)value);

			return System.Convert.ToDecimal(fbType.ToString());
		}
	}
}
