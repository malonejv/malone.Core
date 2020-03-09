using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://lourenco.co.za/blog/2013/07/audit-trails-concurrency-and-soft-deletion-with-entity-framework/
namespace malone.Core.EL
{
    public interface IVersionable
    {
        int Version { get; set; }
    }
}
