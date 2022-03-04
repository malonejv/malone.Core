//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:59</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;

	/// <summary>
	/// Defines the <c>ObjectExtensions</c>.
	/// </summary>
	internal static class ObjectExtensions
	{
		/// <summary>
		/// The IsNull.
		/// </summary>
		/// <typeparam name="T">Type to treat.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsNull<T>(this T instance)
		{
			return instance == null;
		}

		/// <summary>
		/// The IsNotNull.
		/// </summary>
		/// <typeparam name="T">Type to treat.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsNotNull<T>(this T instance)
		{
			return instance != null;
		}

		/// <summary>
		/// The IsDefault.
		/// </summary>
		/// <typeparam name="T">Type to treat.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		internal static bool IsDefault<T>(this T instance)
		{
			return instance.Equals(default(T));
		}

		/// <summary>
		/// The ThrowIfNull.
		/// </summary>
		/// <typeparam name="T">Type to treat.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		internal static T ThrowIfNull<T>(this T instance)
		{
			return ThrowIfNull<T>(instance, nameof(instance));
		}

		/// <summary>
		/// The ThrowIfNull.
		/// </summary>
		/// <typeparam name="T">Type to treat.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <param name="paramName">The paramName <see cref="string"/>.</param>
		internal static T ThrowIfNull<T>(this T instance, string paramName)
		{
			if (instance == null)
			{
				throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
			}
			return instance;
		}

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instances, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="T">Type to treat in the validation.</typeparam>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		internal static TCheckType ThrowIfNotOfType<T, TCheckType>(this T instance)
		{
			return ThrowIfNotOfType<T, TCheckType>(instance, nameof(instance));
		}

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instances, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="T">Type to treat in the validation.</typeparam>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <param name="paramName">The paramName <see cref="string"/>.</param>
		internal static TCheckType ThrowIfNotOfType<T, TCheckType>(this T instance, string paramName)
		{
			if (!(instance is TCheckType))
			{
				throw new ArgumentException(paramName, $"Parameter {paramName} cannot be null.");
			}
			return (TCheckType)Convert.ChangeType(instance, typeof(TCheckType));
		}
	}
}
