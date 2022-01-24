//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:42</date>

using malone.Core.Commons.Configurations.Attributes;
using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations
{
                public static class ConfigurationExtensions
    {
                                internal static ILogger logger;

                                internal static ILogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = ServiceLocator.Current.Get<ILogger>();
                }
                return logger;
            }
        }

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
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }
    }
}
