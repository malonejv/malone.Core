using malone.Core.CL.Configurations.Sections.DbFactory;
using System;
using System.Configuration;
using System.Reflection;

namespace malone.Core.AdoNet.DAL.Database
{
    public class DatabaseFactory
    {

        public static DatabaseFactorySection sectionHandler = (DatabaseFactorySection)ConfigurationManager.GetSection("DatabaseFactoryConfiguration");

        public DatabaseFactory()
        {
        }

        public IDatabase CreateDatabase()
        {

            // Verify a DatabaseFactoryConfiguration line exists in the web.config.
            if (sectionHandler.Name.Length == 0)
            {
                throw new Exception("Database name not defined in DatabaseFactoryConfiguration section of web.config.");
            }
            try
            {
                // Find the class
                Type databaseType = Type.GetType(sectionHandler.Name);
                // Get it's constructor
                ConstructorInfo constructor = databaseType.GetConstructor(new Type[] { });
                // Invoke it's constructor, which returns an instance.
                object[] args = { sectionHandler.ConnectionString };
                IDatabase database = (IDatabase)constructor.Invoke(args);
                // Pass back the instance as a Database
                return database;
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating database " + sectionHandler.Name + ". " + excep.Message);
            }
        }

    }

}
