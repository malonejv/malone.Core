using System.ComponentModel;

namespace malone.Core.Dapper.Database
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
