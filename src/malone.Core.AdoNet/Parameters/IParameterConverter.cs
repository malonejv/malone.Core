namespace malone.Core.AdoNet.Parameters
{
	/// <summary>
	/// Defines the <see cref="IParameterConverter" />.
	/// </summary>
	public interface IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="object"/>.</param>
		/// <returns>The <see cref="object"/>.</returns>
		object Convert(object value);
	}
}
