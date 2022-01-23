using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.AdoNet.Attributes;

namespace malone.Core.AdoNet.Database
{
    public class DbParameterWithValue
    {
        public DbParameterAttribute DbParameter { get; set; }

        public object Value { get; set; }
    }
}
