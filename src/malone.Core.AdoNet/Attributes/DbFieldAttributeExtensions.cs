namespace malone.Core.AdoNet.Attributes
{
	using System;
	using malone.Core.AdoNet.Parameters;

	/// <summary>
	/// Defines the <see cref="T: DbFieldAttributeExtensions" />.
	/// </summary>
	public static class DbFieldAttributeExtensions
	{
		/// <summary>
		/// The Convert.
		/// </summary>
		/// <param name="parameter">The parameter<see cref="T: DbFieldAttribute"/>.</param>
		/// <param name="value">The value<see cref="T: object"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public static object Convert(this DbFieldAttribute parameter, object value)
		{
			object result;
			if (parameter.ValueConverter == null || parameter.ValueConverter.GetInterface(typeof(IParameterConverter).Name) == null)
			{
				result = value;
			}
			else
			{
				IParameterConverter parameterConverter = (IParameterConverter)Activator.CreateInstance(parameter.ValueConverter);
				result = parameterConverter.Convert(value);
			}
			return result;
		}
	}
}
