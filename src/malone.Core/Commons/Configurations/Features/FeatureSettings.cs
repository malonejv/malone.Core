//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:48</date>

using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeatureSettings" />.
    /// </summary>
    public class FeatureSettings
    {
        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        private static FeatureSettings Instance { get; set; }

        /// <summary>
        /// Defines the Configuration.
        /// </summary>
        private ICoreConfiguration Configuration;

        /// <summary>
        /// Defines the FeatureSettingsSection.
        /// </summary>
        private FeatureSettingsSection FeatureSettingsSection;

        /// <summary>
        /// Gets the Features.
        /// </summary>
        public IEnumerable<FeatureElement> Features
        {
            get { return FeatureSettingsSection.Features.Cast<FeatureElement>(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSettings"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="ICoreConfiguration"/>.</param>
        public FeatureSettings(ICoreConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            FeatureSettingsSection = Configuration.GetSection<FeatureSettingsSection>("featureSettings");

            Instance = this;
        }

        /// <summary>
        /// The IsEnabled.
        /// </summary>
        /// <param name="featureName">The featureName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEnabled(string featureName)
        {
            if (featureName == null) throw new ArgumentNullException(nameof(featureName));
            if (featureName == string.Empty) throw new ArgumentException(nameof(featureName));


            var feature = FeatureSettings.Instance.Features.FirstOrDefault(ft => ft.Name == featureName);

            return feature.AllEnabled;
        }

        /// <summary>
        /// The IsEnabled.
        /// </summary>
        /// <param name="featureName">The featureName<see cref="string"/>.</param>
        /// <param name="behaviourName">The behaviourName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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
