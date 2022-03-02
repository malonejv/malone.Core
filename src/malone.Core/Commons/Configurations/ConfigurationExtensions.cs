//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:42</date>

namespace malone.Core.Commons.Configurations
{
	using System;
	using System.Configuration;
	using malone.Core.Commons.Exceptions;
	using malone.Core.Configuration.Attributes;
	using malone.Core.IoC;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="ConfigurationExtensions" />.
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Defines the logger.
		/// </summary>
		internal static ICoreLogger logger;

		/// <summary>
		/// Gets the Logger.
		/// </summary>
		internal static ICoreLogger Logger
		{
			get
			{
				if (logger == null)
				{
					logger = ServiceLocator.Current.Get<ICoreLogger>();
				}
				return logger;
			}
		}

		/// <summary>
		/// The SectionName.
		/// </summary>
		/// <param name="configurationType">The configurationType<see cref="Type"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		internal static string SectionName(this Type configurationType)
		{
			try
			{
				var isConfigSection = configurationType.BaseType.Equals(typeof(ConfigurationSection));
				if (isConfigSection)
				{
					// Get the stringvalue attributes
					SectionNameAttribute[] attribs = configurationType.GetCustomAttributes(typeof(SectionNameAttribute), false) as SectionNameAttribute[];

					// Return the first if there was a match.
					return attribs?.Length > 0 ? attribs[0].Name : null;
				}
				else
				{
					return null;
				}
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.TECH200, configurationType.Name);
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}
	}
}
