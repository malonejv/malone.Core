using System;

namespace malone.Core.DataAccess.Context
{
	public static class IContextExtensions
	{

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instance, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <c>T</c>.</param>
		internal static TCheckType ThrowIfNotOfType<TCheckType>(this IContext instance)
		{
			return ThrowIfNotOfType<TCheckType>(instance, nameof(instance));
		}

		/// <summary>
		/// Validates wether the instances is of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instance, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <see cref="IContext"/>.</param>
		/// <param name="paramName">The paramName <see cref="string"/>.</param>
		internal static TCheckType ThrowIfNotOfType<TCheckType>(this IContext instance, string paramName)
		{
			if (!(instance is TCheckType))
			{
				throw new ArgumentException(paramName, $"Parameter {paramName} cannot be null.");
			}
			return (TCheckType)Convert.ChangeType(instance, typeof(TCheckType));
		}

		/// <summary>
		/// Validates wether the instances derives of type defined with <typeparamref name="TCheckType">TCheckType</typeparamref>.
		/// If it corresponds returns the same instance, in other case returns ArgumentException.
		/// </summary>
		/// <typeparam name="TCheckType">Type used to check if parameter <paramref name="instance">instance</paramref> is of this type.</typeparam>
		/// <param name="instance">The instance of type <see cref="IContext"/>.</param>
		/// <param name="paramName">The parameter name <see cref="string"/>.</param>
		internal static TCheckType ThrowIfNotDeriveOfType<TCheckType>(this IContext instance, string paramName)
		{
			if (!(instance is TCheckType))
			{
				throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
			}

			var typeCheck = typeof(TCheckType);
			var instanceType = instance.GetType();

			if (instanceType != typeof(TCheckType))
			{
				var derived = instanceType.BaseType;
				while (derived != null)
				{
					if (derived == typeof(TCheckType))
						return (TCheckType)instance;

					derived = derived.BaseType;
				}
			}
			else
				return (TCheckType)instance;

			throw new ArgumentException(paramName, $"Parameter {paramName} must inherit of type {typeCheck.Name}.");
		}
	}
}
