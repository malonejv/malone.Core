namespace malone.Core.AdoNet.Parameters
{
	/// <summary>
	/// Defines the <see cref="T: IParameterConverter" />.
	/// </summary>
	public interface IParameterConverter
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		object Convert(object value);
	}
}
