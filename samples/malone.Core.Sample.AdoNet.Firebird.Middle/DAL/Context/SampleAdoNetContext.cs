using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.AdoNet.Context;
using malone.Core.AdoNet.Database;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Context
{
    public class SampleAdoNetContext : AdoNetDbContext
    {
        public SampleAdoNetContext(string connectionStringName) : base(connectionStringName)
        {
        }
    }
}
