//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:49</date>

using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeatureSettingsSection" />.
    /// </summary>
    public class FeatureSettingsSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the Xmlns.
        /// </summary>
        [ConfigurationProperty("xmlns", IsRequired = false)]
        public String Xmlns
        {
            get
            {
                return this["xmlns"] != null ? this["xmlns"].ToString() : string.Empty;
            }
        }

        /// <summary>
        /// Gets the Features.
        /// </summary>
        [ConfigurationProperty("features")]
        public FeaturesElementCollection Features
        {
            get { return (FeaturesElementCollection)this["features"]; }
        }
    }
}
