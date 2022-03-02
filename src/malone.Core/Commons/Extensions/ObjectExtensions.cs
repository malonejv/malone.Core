//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:59</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;

	/// <summary>
	/// Defines the <see cref="ObjectExtensions" />.
	/// </summary>
	internal static class ObjectExtensions
	{
		/// <summary>
		/// The IsNull.
		/// </summary>
		/// <typeparam name="T">.</typeparam>
		/// <param name="@object">The object<see cref="T"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsNull<T>(this T @object)
		{
			return @object == null;
		}

		/// <summary>
		/// The IsNotNull.
		/// </summary>
		/// <typeparam name="T">.</typeparam>
		/// <param name="@object">The object<see cref="T"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsNotNull<T>(this T @object)
		{
			return @object != null;
		}

		/// <summary>
		/// The IsDefault.
		/// </summary>
		/// <typeparam name="T">.</typeparam>
		/// <param name="@object">The object<see cref="T"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsDefault<T>(this T @object)
		{
			return @object.Equals(default(T));
		}

		/// <summary>
		/// The ThrowIfNull.
		/// </summary>
		/// <typeparam name="T">.</typeparam>
		/// <param name="@object">The object<see cref="T"/>.</param>
		/// <param name="paramName">The paramName<see cref="string"/>.</param>
		internal static void ThrowIfNull<T>(this T @object, string paramName)
		{
			if (@object == null)
			{
				throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
			}
		}
	}
}
