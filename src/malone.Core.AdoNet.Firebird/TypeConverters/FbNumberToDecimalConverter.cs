namespace malone.Core.AdoNet.Oracle.Parameters
{
	using System;
	using System.Globalization;
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="FbNumberToDecimalConverter" />.
	/// </summary>
	public class FbNumberToDecimalConverter : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <returns>The <see cref="object"/>.</returns>
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
