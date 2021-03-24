//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:49</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations
{
    /// <summary>
    /// Defines the <see cref="ICoreConfiguration" />.
    /// </summary>
    public interface ICoreConfiguration
    {
        /// <summary>
        /// The GetConnectionString.
        /// </summary>
        /// <param name="connectionStringName">The connectionStringName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetConnectionString(string connectionStringName);

        /// <summary>
        /// The GetSection.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        T GetSection<T>() where T : ConfigurationSection;

        /// <summary>
        /// The GetSection.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sectionName">The sectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        T GetSection<T>(string sectionName) where T : ConfigurationSection;
    }
}
