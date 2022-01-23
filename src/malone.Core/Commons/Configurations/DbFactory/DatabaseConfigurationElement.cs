//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:44</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.DbFactory
{
    /// <summary>
    /// Defines the <see cref="DatabaseConfigurationElement" />.
    /// </summary>
    public class DatabaseConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Providers.
        /// </summary>
        [ConfigurationProperty("providers", IsRequired = true, IsKey = true)]
        public DatabaseProviderElementCollection Providers
        {
            get { return (DatabaseProviderElementCollection)this["providers"]; }
        }
    }
}
