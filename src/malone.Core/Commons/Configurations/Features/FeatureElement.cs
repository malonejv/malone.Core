//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeatureElement" />.
    /// </summary>
    public class FeatureElement : ConfigurationElement
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
        /// Gets or sets a value indicating whether AllEnabled.
        /// </summary>
        [ConfigurationProperty("allEnabled", IsRequired = true)]
        public bool AllEnabled
        {
            get { return (bool)this["allEnabled"]; }
            set { this["allEnabled"] = value; }
        }

        /// <summary>
        /// Gets the Behaviors.
        /// </summary>
        [ConfigurationProperty("behaviors")]
        public BehaviorElementCollection Behaviors
        {
            get { return (BehaviorElementCollection)this["behaviors"]; }
        }
    }
}
