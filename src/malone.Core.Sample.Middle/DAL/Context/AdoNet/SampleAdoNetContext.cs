using malone.Core.AdoNet.DAL.Context;
using malone.Core.AdoNet.DAL.Database;

namespace malone.Core.Sample.Middle.DAL.Context.AdoNet
{
    public class SampleAdoNetContext : AdoNetContext
    {
        public SampleAdoNetContext(DatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
