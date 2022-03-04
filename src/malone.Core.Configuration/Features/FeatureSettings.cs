//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:48</date>

using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Configuration.Features
{
    public class FeatureSettings
    {
        private static FeatureSettings Instance { get; set; }

        private ICoreConfiguration Configuration;

        private FeatureSettingsSection FeatureSettingsSection;

        public IEnumerable<FeatureElement> Features
        {
            get { return FeatureSettingsSection.Features.Cast<FeatureElement>(); }
        }

        public FeatureSettings(ICoreConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Configuration = configuration;
            FeatureSettingsSection = Configuration.GetSection<FeatureSettingsSection>("featureSettings");

            Instance = this;
        }

        public static bool IsEnabled(string featureName)
        {
            if (featureName == null)
            {
                throw new ArgumentNullException(nameof(featureName));
            }

            if (featureName == string.Empty)
            {
                throw new ArgumentException(nameof(featureName));
            }

            var feature = FeatureSettings.Instance.Features.FirstOrDefault(ft => ft.Name == featureName);

            return feature.AllEnabled;
        }

        public static bool IsEnabled(string featureName, string behaviourName)
        {
            bool result = IsEnabled(featureName);

            if (!result)
            {
                var feature = FeatureSettings.Instance.Features.FirstOrDefault(ft => ft.Name == featureName);

                if (feature != null)
                {
                    var behavior = feature.Behaviors.Cast<BehaviorElement>().FirstOrDefault(bh => bh.Name == featureName);

                    result = behavior.IsEnabled;
                }
            }

            return result;
        }
    }
}
