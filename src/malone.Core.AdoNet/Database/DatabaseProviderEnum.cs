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
        [Description("malone.Core.AdoNet.SqlServer.SqlDatabase, malone.Core.AdoNet.SqlServer")]
        SqlServer,
        [Description("malone.Core.AdoNet.Oracle.OracleDatabase, malone.Core.AdoNet.Oracle")]
        Oracle,
        [Description("malone.Core.AdoNet.Firebird.FbDatabase, malone.Core.AdoNet.Firebird")]
        Firebird
    }
}
