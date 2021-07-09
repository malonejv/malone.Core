//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:43</date>

using System.ComponentModel;

namespace malone.Core.Commons.Configurations
{
    /// <summary>
    /// Defines the CoreModules.
    /// </summary>
    public enum CoreModules
    {
        /// <summary>
        /// Defines the Basics.
        /// </summary>
        [Description("Basics")]
        Basics,
        /// <summary>
        /// Defines the Log4NetLogger.
        /// </summary>
        [Description("Log4NetLogger")]
        Log4NetLogger,
        /// <summary>
        /// Defines the Features.
        /// </summary>
        [Description("Features")]
        Features,
        /// <summary>
        /// Defines the IdentityAdoNetSqlServer.
        /// </summary>
        [Description("IdentityAdoNetSqlServer")]
        IdentityAdoNetSqlServer,
        /// <summary>
        /// Defines the IdentityEntityFramework.
        /// </summary>
        [Description("IdentityEntityFramework")]
        IdentityEntityFramework,
        /// <summary>
        /// Defines the IdentityDapper.
        /// </summary>
        [Description("IdentityDapper")]
        IdentityDapper,
    }
}
