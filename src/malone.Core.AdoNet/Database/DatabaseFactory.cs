using malone.Core.Commons.Configurations;
using malone.Core.Commons.Configurations.DbFactory;
using malone.Core.Commons.Helpers.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace malone.Core.AdoNet.Database
{
    /// <summary>
    /// Crea una base de datos Ado.Net de acuerdo a la seccion de configuracion.
    /// </summary>
    /// <![CDATA[
    /// 
    /// ]]>
    public class DatabaseFactory
    {
        private ICoreConfiguration Configuration { get; set; }
        private DatabaseConfigurationElement DatabaseConfiguration { get; set; }

        public DatabaseFactory(ICoreConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;

            var coreSettings = Configuration.GetSection<CoreSettingsSection>();
            if (coreSettings == null) throw new ConfigurationErrorsException(nameof(coreSettings));

            DatabaseConfiguration = coreSettings.DatabaseConfiguration;
        }

        //TODO: Terminar
        public IDatabase CreateDatabase(string connectionStringName)
        {
            // Verify a DatabaseFactoryConfiguration line exists in the web.config.
            if (DatabaseConfiguration.Providers == null || DatabaseConfiguration.Providers.Count == 0)
            {
                //TODO: Utilizar errores del Core
                throw new Exception("Database Provider not defined in DatabaseFactoryConfiguration section of config file.");
            }

            try
            {
                DatabaseProviderElement providerElement = DatabaseConfiguration.Providers
                                                                        .Cast<DatabaseProviderElement>()
                                                                        .Where(p => p.ConnectionStringName == connectionStringName)
                                                                        .FirstOrDefault();

                DatabaseProvider provider = default(DatabaseProvider);
                Enum.TryParse<DatabaseProvider>(providerElement.Name, out provider);

                // Find the class
                Type databaseType = Type.GetType(provider.GetDescription());

                // Get it's constructor
                ConstructorInfo constructor = databaseType.GetConstructor(new Type[] { typeof(string) });

                var connectionString = Configuration.GetConnectionString(connectionStringName);

                // Invoke it's constructor, which returns an instance.
                object[] args = { connectionString };
                IDatabase database = (IDatabase)constructor.Invoke(args);

                // Pass back the instance as a Database
                return database;
            }
            catch (ArgumentException)
            {
                //TODO: Utilizar errores del Core
                throw new Exception("Not a valid Database Provider Name.");
            }
            catch (Exception excep)
            {
                //TODO: Utilizar errores del Core
                throw new Exception("Error instantiating database " + connectionStringName + ". " + excep.Message);
            }
        }
    }

}
