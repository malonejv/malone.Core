using System;

namespace malone.Core.DataAccess.Context
{
	public static class IContextExtensions
	{

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instances, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		internal static TCheckType ThrowIfNotOfType<TCheckType>(this IContext instance)
		{
			return ThrowIfNotOfType<TCheckType>(instance, nameof(instance));
		}

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instances, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		/// <param name="paramName">The paramName <see cref="string"/>.</param>
		internal static TCheckType ThrowIfNotOfType<TCheckType>(this IContext instance, string paramName)
		{
			if (!(instance is TCheckType))
			{
				throw new ArgumentException(paramName, $"Parameter {paramName} cannot be null.");
			}
			return (TCheckType)Convert.ChangeType(instance, typeof(TCheckType));
		}
	}
}
