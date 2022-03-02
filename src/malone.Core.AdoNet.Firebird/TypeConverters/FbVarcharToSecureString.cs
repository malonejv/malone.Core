namespace malone.Core.AdoNet.Oracle.TypeConverters
{
	using malone.Core.AdoNet.Parameters;
	using malone.Core.Commons.Helpers.Extensions;

	/// <summary>
	/// Defines the <see cref="T: FbVarcharToSecureString" />.
	/// </summary>
	public class FbVarcharToSecureString : IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public object Convert(object value)
		{
			return value != null ? value.ToString().ToSecureString() : null;
		}
	}
}
