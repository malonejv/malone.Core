using System.Reflection;

namespace malone.Core.CL.Configurations.Features
{
    public static class FeatureExtensions
    {

        public static bool IsEnabled(this MethodBase method)
        {
            FeatureDescriptionAttribute attr = (FeatureDescriptionAttribute)method.GetCustomAttributes(typeof(FeatureDescriptionAttribute), true)[0];

            return FeatureSettings.IsEnabled(attr.Feature,attr.Behavior);
        }

    }
}
