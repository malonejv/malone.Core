//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:53</date>

namespace malone.Core.Commons.Exceptions
{
	using System;
	using System.Reflection;
	using malone.Core.IoC;

	/// <summary>
	/// Defines the <see cref="CoreExceptionFactory" />.
	/// </summary>
	internal static class CoreExceptionFactory
	{
		/// <summary>
		/// Defines the errorLocalizationHandler.
		/// </summary>
		internal static IErrorLocalizationHandler errorLocalizationHandler;

		/// <summary>
		/// Gets the ErrorLocalizationHandler.
		/// </summary>
		internal static IErrorLocalizationHandler ErrorLocalizationHandler
		{
			get
			{
				if (errorLocalizationHandler == null)
				{
					errorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
				}
				return errorLocalizationHandler;
			}
		}

		/// <summary>
		/// The CreateException.
		/// </summary>
		/// <typeparam name="TException">.</typeparam>
		/// <param name="code">The code<see cref="CoreErrors"/>.</param>
		/// <param name="args">The args<see cref="object[]"/>.</param>
		/// <returns>The <see cref="TException"/>.</returns>
		internal static TException CreateException<TException>(CoreErrors code, params object[] args) where TException : BaseException
		{
			var suportId = Guid.NewGuid();
			string message = ErrorLocalizationHandler.GetString(code, args);

			// Find the class
			Type exceptionType = typeof(TException);

			// Get it's constructor
			ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(string) });

			// Invoke it's constructor, which returns an instance.
			object[] constructorParams = { message };

			TException baseException = (TException)constructor.Invoke(constructorParams);

			baseException.Data.Add(BaseException.SUPPORT_ID, suportId);
			baseException.Data.Add(BaseException.ERROR_CODE, code.ToString());

			return baseException;
		}

		/// <summary>
		/// The CreateException.
		/// </summary>
		/// <typeparam name="TException">.</typeparam>
		/// <param name="innerException">The innerException<see cref="Exception"/>.</param>
		/// <param name="code">The code<see cref="CoreErrors"/>.</param>
		/// <param name="args">The args<see cref="object[]"/>.</param>
		/// <returns>The <see cref="TException"/>.</returns>
		internal static TException CreateException<TException>(Exception innerException, CoreErrors code, params object[] args) where TException : BaseException
		{
			var suportId = Guid.NewGuid();
			string message = ErrorLocalizationHandler.GetString(code, args);

			// Find the class
			Type exceptionType = typeof(TException);

			// Get it's constructor
			ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

			// Invoke it's constructor, which returns an instance.
			object[] constructorParams = { message, innerException };

			TException baseException = (TException)constructor.Invoke(constructorParams);

			baseException.Data.Add(BaseException.SUPPORT_ID, suportId);
			baseException.Data.Add(BaseException.ERROR_CODE, code.ToString());

			return baseException;
		}
	}
}
