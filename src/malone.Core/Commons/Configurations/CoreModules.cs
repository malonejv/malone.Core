using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Configurations
{
    public enum CoreModules
    {
        [Description("Basics")]
        Basics,
        [Description("Log4NetLogger")]
        Log4NetLogger,
        [Description("Features")]
        Features,
        [Description("IdentityAdoNetSqlServer")]
        IdentityAdoNetSqlServer,
        [Description("IdentityEntityFramework")]
        IdentityEntityFramework
    }
}
