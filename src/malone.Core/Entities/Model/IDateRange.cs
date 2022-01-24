//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:18</date>

using System;

namespace malone.Core.Entities.Model
{
                public interface IDateRange
    {
                                DateTime? FromDate { get; set; }

                                DateTime? ToDate { get; set; }
    }
}
