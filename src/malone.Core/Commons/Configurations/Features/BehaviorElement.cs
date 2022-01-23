//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:46</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="BehaviorElement" />.
    /// </summary>
    public class BehaviorElement : ConfigurationElement
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

        /// <summary>
        /// Gets or sets a value indicating whether IsEnabled.
        /// </summary>
        [ConfigurationProperty("isEnabled", IsRequired = true)]
        public bool IsEnabled
        {
            get { return (bool)this["isEnabled"]; }
            set { this["isEnabled"] = value; }
        }
    }
}
