using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.Features
{

    public class FeatureSettingsSection : ConfigurationSection
    {

        [ConfigurationProperty("features")]
        public FeaturesElementCollection Features
        {
            get { return (FeaturesElementCollection)this["features"]; }
        }

    }

}
