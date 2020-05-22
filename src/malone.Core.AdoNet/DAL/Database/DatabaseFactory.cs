using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.DbFactory;
using malone.Core.CL.Helpers.Extensions;
using System;
using System.Reflection;

namespace malone.Core.AdoNet.DAL.Database
{
    public class DatabaseFactory
    {
        private ICoreConfiguration Configuration { get; set; }
        private DatabaseConfiguration DatabaseConfiguration { get; set; }

        public DatabaseFactory(ICoreConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            DatabaseConfiguration = Configuration.GetSection<DatabaseConfiguration>(DatabaseConfiguration.SectionName());
        }

        public IDatabase CreateDatabase()
        {
            // Verify a DatabaseFactoryConfiguration line exists in the web.config.
            if (string.IsNullOrEmpty(DatabaseConfiguration.Provider.Trim()))
            {
                throw new Exception("Database name not defined in DatabaseFactoryConfiguration section of web.config.");
            }
            try
            {
                DatabaseProvider provider = default(DatabaseProvider);
                Enum.TryParse<DatabaseProvider>(DatabaseConfiguration.Provider, out provider);

                // Find the class
                Type databaseType = Type.GetType(provider.GetDescription());

                // Get it's constructor
                ConstructorInfo constructor = databaseType.GetConstructor(new Type[] { });

                // Invoke it's constructor, which returns an instance.
                string connectionStringName = Configuration.GetConnectionString(DatabaseConfiguration.ConnectionStringName);
                object[] args = { connectionStringName };
                IDatabase database = (IDatabase)constructor.Invoke(args);

                // Pass back the instance as a Database
                return database;
            }
            catch (ArgumentException)
            {
                throw new Exception("Not a valid Database Provider Name.");
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating database " + DatabaseConfiguration.Provider + ". " + excep.Message);
            }
        }

    }

}
