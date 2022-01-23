//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:50</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.Modules
{
    /// <summary>
    /// Defines the <see cref="ModuleElement" />.
    /// </summary>
    public class ModuleElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}
