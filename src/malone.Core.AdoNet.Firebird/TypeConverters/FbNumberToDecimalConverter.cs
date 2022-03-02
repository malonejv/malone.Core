namespace malone.Core.AdoNet.Oracle.Parameters
{
	using System;
	using System.Globalization;
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="T: FbNumberToDecimalConverter" />.
	/// </summary>
	public class FbNumberToDecimalConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public object Convert(object value)
		{
			if (value is DBNull)
			{
				return 0m;
			}
			return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}
	}
}
