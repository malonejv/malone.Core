using malone.Core.Commons.Configurations.Attributes;
using malone.Core.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Configurations
{
    public static class ConfigurationExtensions
    {
        public static string SectionName(this Type configurationType)
        {
            try
            {
                var isConfigSection = configurationType.BaseType.Equals(typeof(ConfigurationSection));
                if (isConfigSection)
                {
                    // Get the stringvalue attributes
                    SectionNameAttribute[] attribs = configurationType.GetCustomAttributes(typeof(SectionNameAttribute), false) as SectionNameAttribute[];

                    // Return the first if there was a match.
                    return attribs.Length > 0 ? attribs[0].Name : null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //TODO: Mejorar especificación del error
                throw new TechnicalException<CoreErrors>(CoreErrors.E800);
            }
        }
    }
}
