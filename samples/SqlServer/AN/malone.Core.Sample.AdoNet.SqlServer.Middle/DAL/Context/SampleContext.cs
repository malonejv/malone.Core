using malone.Core.Identity.AdoNet.SqlServer.Context;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Context
{
	public class SampleContext : CoreIdentityDbContext//CoreDbContext
    {
        public SampleContext(string connectionStringName) : base(connectionStringName)
        {
        }
    }
}
