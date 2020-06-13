using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.Commons.Configurations
{

    public interface ICoreConfiguration
    {
        string GetConnectionString(string connectionStringName);

       T GetSection<T>() where T: ConfigurationSection;

        T GetSection<T>(string sectionName) where T : ConfigurationSection;
    }
}
