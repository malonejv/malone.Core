using System.Data.Entity;
using malone.Core.Identity.AdoNet.SqlServer.Context;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AN.SqlServer.Middle.DAL.Context
{
    public class SampleContext : CoreIdentityDbContext //CoreDbContext
    {
        public SampleContext() : base("SampleConnection")
        {
        }

        public SampleContext(string connectionStringName)
        : base(connectionStringName)
        {
        }


        public DbSet<TodoList> TodoLists { get; set; }
    }

}
