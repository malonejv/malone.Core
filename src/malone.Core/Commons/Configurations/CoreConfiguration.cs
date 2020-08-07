using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;

namespace malone.Core.Commons.Configurations
{
    public class CoreConfiguration : ICoreConfiguration
    {
        //protected ILogger Logger { get; set; }
        internal IErrorLocalizationHandler ErrorLocalizationHandler { get; set; }

        public CoreConfiguration()//(ILogger logger)
        {
            ErrorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
            //Logger = logger;
        }

        public string GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionString.ConnectionString;
        }


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
