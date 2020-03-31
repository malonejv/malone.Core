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

        public static bool IsFeatureEnabled(this MethodBase method)

        {

            FeatureDescriptionAttribute attr = (FeatureDescriptionAttribute)method.GetCustomAttributes(typeof(FeatureDescriptionAttribute), true)[0];



            var feature = FeatureSettings.GetFeatures().Cast<FeatureElement>().FirstOrDefault(ft => ft.Name == attr.Feature);



            return feature.AllEnabled;

        }



        public static bool IsBehaviorEnabled(this MethodBase method)

        {

            FeatureDescriptionAttribute attr = (FeatureDescriptionAttribute)method.GetCustomAttributes(typeof(FeatureDescriptionAttribute), true)[0];



            bool result = IsFeatureEnabled(method);



            if (!result)

            {

                var feature = FeatureSettings.GetFeatures().Cast<FeatureElement>().FirstOrDefault(ft => ft.Name == attr.Feature);

                if (feature != null)

                {

                    var behavior = feature.Behaviors.Cast<BehaviorElement>().FirstOrDefault(bh => bh.Name == attr.Behavior);

                    result = behavior.IsEnabled;

                }

            }



            return result;

        }



    }
}
