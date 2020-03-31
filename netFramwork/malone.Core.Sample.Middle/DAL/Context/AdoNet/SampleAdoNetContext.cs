using malone.Core.DAL.AdoNet.Context;
using malone.Core.DAL.AdoNet.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.Middle.DAL.Context.AdoNet
{
    public class SampleAdoNetContext : AdoNetContext
    {
        public SampleAdoNetContext(DatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
