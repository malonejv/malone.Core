using malone.Core.CL.Configurations.Attributes;
using malone.Core.CL.Configurations.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Configurations.Extensions
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
