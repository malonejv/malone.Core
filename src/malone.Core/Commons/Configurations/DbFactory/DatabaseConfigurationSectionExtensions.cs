using malone.Core.CL.Configurations.Attributes;
using System;

namespace malone.Core.CL.Configurations.DbFactory
{
    public static class DatabaseConfigurationSectionExtensions
    {
        public static string SectionName(this DatabaseConfigurationSection dbConfiguration)
        {
            // Get the type
            Type dbConfigurationType = typeof(DatabaseConfigurationSection);

            // Get the stringvalue attributes
            SectionNameAttribute[] attribs = dbConfigurationType.GetCustomAttributes(typeof(SectionNameAttribute), false) as SectionNameAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Name : null;
        }
    }
}
