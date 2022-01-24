//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:49</date>

using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations.Features
{
                public class FeatureSettingsSection : ConfigurationSection
    {
                                [ConfigurationProperty("xmlns", IsRequired = false)]
        public String Xmlns
        {
            get
            {
                return this["xmlns"] != null ? this["xmlns"].ToString() : string.Empty;
            }
        }

                                [ConfigurationProperty("features")]
        public FeaturesElementCollection Features
        {
            get { return (FeaturesElementCollection)this["features"]; }
        }
    }
}
