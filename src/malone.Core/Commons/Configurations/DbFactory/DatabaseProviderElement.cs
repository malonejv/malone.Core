//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:45</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.DbFactory
{
    /// <summary>
    /// Defines the <see cref="DatabaseProviderElement" />.
    /// </summary>
    public class DatabaseProviderElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [ConfigurationProperty("name", IsKey = false, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the ConnectionStringName.
        /// </summary>
        [ConfigurationProperty("connectionStringName", IsKey = true, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string)this["connectionStringName"]; }
            set { this["connectionStringName"] = value; }
        }
    }
}
