namespace malone.Core.AdoNet.Oracle.Parameters
{
	using System;
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="FbNumberToFloatConverter" />.
	/// </summary>
	public class FbNumberToFloatConverter : IParameterConverter
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
				return 0f;
			}
			return ((IConvertible)value).ToSingle(null);
		}
	}
}
