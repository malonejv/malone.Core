using malone.Core.Identity.Dapper.Context;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Context
{
    public class SampleContext : CoreIdentityDbContext//CoreDbContext
    {
        public SampleContext(string connectionStringName) : base(connectionStringName)
        {
        }
    }
}
