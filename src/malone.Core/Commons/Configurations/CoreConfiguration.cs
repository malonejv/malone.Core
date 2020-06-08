using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.Commons.Configurations
{
    public class CoreConfiguration : ICoreConfiguration
    {
        public CoreConfiguration()
        {
        }

        public string GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionString.ConnectionString;
        }


        public T GetSection<T>()
            where T: ConfigurationSection
        {
            var sectionName = typeof(T).SectionName();
            if (sectionName == null) throw new ArgumentNullException(nameof(sectionName));

            var section = ConfigurationManager.GetSection(sectionName);

            if (section == null)
            {
                return default(T);
            }
            return (T)section;
        }

        public T GetSection<T>(string sectionName)
            where T: ConfigurationSection
        {
            if (sectionName == null) throw new ArgumentNullException(nameof(sectionName));

            var section = ConfigurationManager.GetSection(sectionName);

            if (section == null)
            {
                return default(T);
            }
            return (T)section;
        }
    }
}
