//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:58</date>

using malone.Core.Entities.Model;
using System;

namespace malone.Core.Commons.Helpers.Extensions
{
                public static class DateRangeExtensions
    {
                                                        public static bool Contains(this IDateRange range, DateTime date)
        {
            bool result = false;

            if (range.IsValidRange())
                result = range.FromDate <= date && range.ToDate >= date;

            return result;
        }

                                                        public static bool Contains(this IDateRange range, IDateRange comparisonRange)
        {
            bool result = false;

            if (range.IsValidRange())
                result = range.Contains(comparisonRange.FromDate.Value) && range.Contains(comparisonRange.ToDate.Value);

            return result;
        }

                                                        public static bool HasOverlap(this IDateRange range, IDateRange comparisonRange)
        {
            bool result = false;

            if (range.IsValidRange() & comparisonRange.IsValidRange())
                result = range.Contains(comparisonRange.FromDate.Value)
                       || range.Contains(comparisonRange.ToDate.Value)
                       || range.Contains(comparisonRange)
                       || comparisonRange.Contains(range);

            return result;
        }

                                                                public static bool HasOverlap(this IDateRange range, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            DateRange comparisonRange = new DateRange() { FromDate = from, ToDate = to };

            return range.HasOverlap(comparisonRange);
        }

                                                public static bool IsValidRange(this IDateRange range)
        {
            bool result = false;

            result = range.FromDate.HasValue && range.ToDate.HasValue;

            return result;
        }
    }
}
