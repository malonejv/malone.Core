using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.DAL.Database
{
    public enum DatabaseProvider
    {
        [Description("malone.Core.DAL.AdoNet.Provider.SqlServer.SqlDatabase")]
        SqlServer,
        [Description("malone.Core.DAL.AdoNet.Provider.Oracle.OracleDatabase")]
        Oracle
    }
}
