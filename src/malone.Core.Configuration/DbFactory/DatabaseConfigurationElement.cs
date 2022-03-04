//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:44</date>

using System.Configuration;

namespace malone.Core.Configuration.DbFactory
{
    public class DatabaseConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("providers", IsRequired = true, IsKey = true)]
        public DatabaseProviderElementCollection Providers
        {
            get { return (DatabaseProviderElementCollection)this["providers"]; }
        }
    }
}
