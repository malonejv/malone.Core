namespace malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Context
{
    public class SampleContext : CoreIdentityDbContext//CoreDbContext
    {
        public SampleContext(string connectionStringName) : base(connectionStringName)
        {
        }
    }
}
