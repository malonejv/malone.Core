using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations.Sections
{
    public class FeatureSettings
    {
        private ICoreConfiguration Configuration;

        protected FeatureSettingsSection FeatureSettingsSection
        {
            get
            {
                return Configuration.GetSection<FeatureSettingsSection>("featureSettings");
            }
        }

        public FeatureSettings(ICoreConfiguration configuration)
        {
            Configuration = configuration;
        }

        public  FeaturesElementCollection GetFeatures()
        {
            return FeatureSettingsSection.Features;
        }

    }
}
