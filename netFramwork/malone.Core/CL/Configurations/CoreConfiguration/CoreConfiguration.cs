using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.CoreConfiguration
{
    public class CoreConfiguration : ICoreConfiguration
    {
        public CoreConfiguration()
        {
        }

        public T GetSection<T>(string sectionKey)
        {
            if (sectionKey == null) throw new ArgumentNullException(nameof(sectionKey));

            var section = ConfigurationManager.GetSection(sectionKey);

            if (section == null)
            {
                return default(T);
            }
            return (T)section;
        }
    }
}
