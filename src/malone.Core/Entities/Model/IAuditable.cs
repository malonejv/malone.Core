//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:16</date>

using System;

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
