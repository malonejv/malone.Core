//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:49</date>

using System.Configuration;

namespace malone.Core.Configuration
{
    public interface ICoreConfiguration
    {
        string GetConnectionString(string connectionStringName);

        T GetSection<T>() where T : ConfigurationSection;

        T GetSection<T>(string sectionName) where T : ConfigurationSection;
    }
}
