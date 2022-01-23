//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:43</date>

using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations
{
    /// <summary>
    /// Defines the <see cref="CoreConfiguration" />.
    /// </summary>
    public class CoreConfiguration : ICoreConfiguration
    {
        /// <summary>
        /// Gets or sets the ErrorLocalizationHandler.
        /// </summary>
        internal IErrorLocalizationHandler ErrorLocalizationHandler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreConfiguration"/> class.
        /// </summary>
        public CoreConfiguration()//(ILogger logger)
        {
            ErrorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
        }

        /// <summary>
        /// The GetConnectionString.
        /// </summary>
        /// <param name="connectionStringName">The connectionStringName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// The GetSection.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public T GetSection<T>()
            where T : ConfigurationSection
        {
            try
            {
                var sectionName = typeof(T).SectionName();
                if (sectionName == null)
                {
                    ErrorLocalizationHandler.GetString(CoreErrors.CONF2, typeof(T).Name);
                    throw new ConfigurationErrorsException();
                }

                var section = ConfigurationManager.GetSection(sectionName);

                if (section == null)
                {
                    return default(T);
                }
                return (T)section;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.CONF2, typeof(T).Name);
                //if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        /// <summary>
        /// The GetSection.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sectionName">The sectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        public T GetSection<T>(string sectionName)
            where T : ConfigurationSection
        {
            try
            {
                if (sectionName == null) throw new ArgumentNullException(nameof(sectionName));

                var section = ConfigurationManager.GetSection(sectionName);

                if (section == null)
                {
                    return default(T);
                }
                return (T)section;
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.CONF2, typeof(T).Name);
                //if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }
    }
}
