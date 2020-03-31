using malone.Core.CL.Configurations.ConnectionString;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations.Extensions
{
    public static class IConfigurationExtensions
    {
        public static ConnectionStringSettingsDictionary GetConnectionStrings(this IConfiguration configuration, String section = "ConnectionStrings")
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var connectionStringCollection = configuration.GetSection(section).Get<ConnectionStringSettingsDictionary>();
            if (connectionStringCollection == null)
            {
                return new ConnectionStringSettingsDictionary();
            }

            return connectionStringCollection;
        }

        public static ConnectionStringSettings GetConnectionStringSettings(this IConfiguration configuration, String name, String section = "ConnectionStrings")
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            ConnectionStringSettings connectionStringSettings;

            var connectionStringCollection = configuration.GetSection(section).Get<ConnectionStringSettingsDictionary>();
            if (connectionStringCollection == null ||
                !connectionStringCollection.TryGetValue(name, out connectionStringSettings))
            {
                connectionStringSettings = null;
            }

            return connectionStringSettings;
        }
    }
}
