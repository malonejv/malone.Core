using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://lourenco.co.za/blog/2013/07/audit-trails-concurrency-and-soft-deletion-with-entity-framework/
namespace malone.Core.Entities.Model
{
    public interface IAuditable
    {
        Guid CreatedById { get; set; }
        DateTime CreatedDateTime { get; set; }
        Guid EditedById { get; set; }
        DateTime EditedDateTime { get; set; }
    }
}
