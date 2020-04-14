using malone.Core.DAL.AdoNet.Provider;
using malone.Core.DAL.AdoNet.Provider.Oracle;
using malone.Core.DAL.AdoNet.Provider.SqlServer;
using System.Configuration;

namespace malone.Core.DAL.AdoNet.Factory
{
    public static class ProviderNames
    {
        public const string SqlProvider = "System.Data.SqlClient";
        public const string OracleProvider = "Oracle.ManagedDataAccess.Client";
    }

    public class DatabaseFactory
    {
        private ConnectionStringSettings ConnectionStringSettings { get; set; }
    
        public DatabaseFactory(string connectionStringName)
        {
            ConnectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        public IDatabase CreateDatabase()
        {
            switch (ConnectionStringSettings.ProviderName)
            {
                case ProviderNames.SqlProvider:
                    return new SqlDatabase(ConnectionStringSettings.ConnectionString);
                case ProviderNames.OracleProvider:
                    return new OracleDatabase(ConnectionStringSettings.ConnectionString);
                default:
                    return null;
            }
        }

    }

}
