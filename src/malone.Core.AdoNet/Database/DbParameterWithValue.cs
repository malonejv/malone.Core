namespace malone.Core.AdoNet.Database
{
	using malone.Core.AdoNet.Attributes;

	/// <summary>
	/// Defines the <see cref="DbParameterWithValue" />.
	/// </summary>
	public class DbParameterWithValue
	{
		/// <summary>
		/// Gets or sets the DbParameter.
		/// </summary>
		public ParameterAttribute DbParameter { get; set; }

		/// <summary>
		/// Gets or sets the Value.
		/// </summary>
		public object Value { get; set; }
	}
}
