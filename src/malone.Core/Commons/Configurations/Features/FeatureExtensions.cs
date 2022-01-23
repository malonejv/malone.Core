//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System.Reflection;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeatureExtensions" />.
    /// </summary>
    public static class FeatureExtensions
    {
        /// <summary>
        /// The IsEnabled.
        /// </summary>
        /// <param name="method">The method<see cref="MethodBase"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEnabled(this MethodBase method)
        {
            FeatureDescriptionAttribute attr = (FeatureDescriptionAttribute)method.GetCustomAttributes(typeof(FeatureDescriptionAttribute), true)[0];

            return FeatureSettings.IsEnabled(attr.Feature, attr.Behavior);
        }
    }
}
