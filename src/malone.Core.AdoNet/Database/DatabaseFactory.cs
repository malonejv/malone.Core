﻿using malone.Core.Commons.Configurations;
using malone.Core.Commons.Configurations.DbFactory;
using malone.Core.Commons.Helpers.Extensions;
using System;
using System.Configuration;
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
            if(coreSettings == null) throw new ConfigurationErrorsException(nameof(coreSettings));

            DatabaseConfiguration = coreSettings.DatabaseConfiguration;
        }

        //TODO: Terminar
        public IDatabase CreateDatabase()
        {
            //// Verify a DatabaseFactoryConfiguration line exists in the web.config.
            //if (DatabaseConfiguration.Providers == null || DatabaseConfiguration.Providers.Count == 0)
            //{
            //    throw new Exception("Database Provider not defined in DatabaseFactoryConfiguration section of web.config.");
            //}
            //try
            //{
            //    DatabaseProvider provider = default(DatabaseProvider);
            //    //TODO: Corregir. Solo obtengo el 1er proveedor.
            //    DatabaseConfiguration.Providers
            //    Enum.TryParse<DatabaseProvider>(DatabaseConfiguration.Providers, out provider);

            //    // Find the class
            //    Type databaseType = Type.GetType(provider.GetDescription());

            //    // Get it's constructor
            //    ConstructorInfo constructor = databaseType.GetConstructor(new Type[] { });

            //    // Invoke it's constructor, which returns an instance.
            //    string connectionStringName = Configuration.GetConnectionString(DatabaseConfiguration.ConnectionStringName);
            //    object[] args = { connectionStringName };
            //    IDatabase database = (IDatabase)constructor.Invoke(args);

            //    // Pass back the instance as a Database
            //    return database;
            //}
            //catch (ArgumentException)
            //{
            //    throw new Exception("Not a valid Database Provider Name.");
            //}
            //catch (Exception excep)
            //{
            //    throw new Exception("Error instantiating database " + DatabaseConfiguration.Provider + ". " + excep.Message);
            //}
            return null;
        }

    }

}