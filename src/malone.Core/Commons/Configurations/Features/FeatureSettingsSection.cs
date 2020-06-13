using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

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
