//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System.Reflection;

namespace malone.Core.Configuration.Features
{
    public static class FeatureExtensions
    {
        public static bool IsEnabled(this MethodBase method)
        {
            FeatureDescriptionAttribute attr = (FeatureDescriptionAttribute)method.GetCustomAttributes(typeof(FeatureDescriptionAttribute), true)[0];

            return FeatureSettings.IsEnabled(attr.Feature, attr.Behavior);
        }
    }
}
