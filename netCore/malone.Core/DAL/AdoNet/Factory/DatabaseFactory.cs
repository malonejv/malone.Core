using malone.Core.CL.Configurations;
using malone.Core.DAL.AdoNet.DatabasConnection;
using malone.Core.DAL.AdoNet.OracleClient;
using malone.Core.DAL.AdoNet.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace malone.Core.DAL.AdoNet.Factory
{
    public class DatabaseFactory
    {
        private ConnectionStringSettings ConnectionStringSettings { get; set; }
    
        public DatabaseFactory(IConfiguration configuration, string connectionStringName)
        {
            ConnectionStringSettings = configuration.GetConnectionStringSettings(connectionStringName);
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

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {

            switch (ConnectionStringSettings.ProviderName)
            {
                case ProviderNames.SqlProvider:
                    return new SqlParameter()
                    {
                        ParameterName = name,
                        Value = value,
                        DbType = dbType
                    };
                case ProviderNames.OracleProvider:
                    return new OracleParameter()
                    {
                        ParameterName = name,
                        Value = value,
                        DbType = dbType
                    };
                default:
                    return null;
            }
        }
        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
        {

            switch (ConnectionStringSettings.ProviderName)
            {
                case ProviderNames.SqlProvider:
                    return new SqlParameter()
                    {
                        ParameterName = name,
                        Size = size,
                        Value = value,
                        DbType = dbType
                    };
                case ProviderNames.OracleProvider:
                    return new OracleParameter()
                    {
                        ParameterName = name,
                        Size = size,
                        Value = value,
                        DbType = dbType
                    };
                default:
                    return null;
            }
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {

            switch (ConnectionStringSettings.ProviderName)
            {
                case ProviderNames.SqlProvider:
                    return new SqlParameter()
                    {
                        ParameterName = name,
                        Size = size,
                        Value = value,
                        DbType = dbType,
                        Direction = direction
                    };
                case ProviderNames.OracleProvider:
                    return new OracleParameter()
                    {
                        ParameterName = name,
                        Size = size,
                        Value = value,
                        DbType = dbType,
                        Direction = direction
                    };
                default:
                    return null;
            }
        }

    }

    public static class ProviderNames
    {
        public const string SqlProvider = "system.Data.SqlClient";
        public const string OracleProvider = "system.Data.OracleClient";
    }
}
