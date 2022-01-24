//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:18</date>


//http://lourenco.co.za/blog/2013/07/audit-trails-concurrency-and-soft-deletion-with-entity-framework/

namespace malone.Core.Entities.Model
{
                public interface IVersionable
    {
                                int Version { get; set; }
    }
}
