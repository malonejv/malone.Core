//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:16</date>

using System;

namespace malone.Core.Entities.Model
{
    public class DateRange : IDateRange
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
