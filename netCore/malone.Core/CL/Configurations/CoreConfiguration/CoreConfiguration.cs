using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations.CoreConfiguration
{
    public class CoreConfiguration : ICoreConfiguration
    {
        private IConfiguration Configuration;

        public CoreConfiguration(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
        }

        public T GetSection<T>(string sectionKey)
        {
            if (sectionKey == null) throw new ArgumentNullException(nameof(sectionKey));

            return Configuration.GetSection(sectionKey).Get<T>();
        }
    }
}
