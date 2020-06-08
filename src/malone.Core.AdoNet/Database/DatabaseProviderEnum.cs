using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.Database
{
    public enum DatabaseProvider
    {
        [Description("malone.Core.DataAccess.AdoNet.Provider.SqlServer.SqlDatabase")]
        SqlServer,
        [Description("malone.Core.DataAccess.AdoNet.Provider.Oracle.OracleDatabase")]
        Oracle
    }
}
