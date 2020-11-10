using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.AdoNet.Context;
using malone.Core.AdoNet.Database;
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
