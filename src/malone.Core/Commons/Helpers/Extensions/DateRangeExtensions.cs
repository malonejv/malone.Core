using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.Commons.Helpers.Extensions
{
    public static class DateRangeExtensions
    {
        /// <summary>
        ///Validates whether a dates is inside a range.
        ///</summary>
        ///<param name="range">Date range (from - to).</param>
        ///<param name="date">Fecha a evaluar.</param>
        ///<returns>Returns a boolean value indicating whether a date is inside a date range.</returns>
        ///<remarks></remarks>
        public static bool Contains(this IDateRange range, DateTime date)
        {
            bool result = false;

            if (range.IsValidRange())
                result = range.FromDate <= date && range.ToDate >= date;

            return result;
        }

        /// <summary>
        ///Validates whether both dates of a comparison date range are inside a date range.
        ///</summary>
        ///<param name="range">Date range (from - to).</param>
        ///<param name="comparisonRange">Date range to compare.</param>
        ///<returns>Returns a boolean value indicating whether both dates of the comparison date range are inside a date range.</returns>
        ///<remarks></remarks>
        public static bool Contains(this IDateRange range, IDateRange comparisonRange)
        {
            bool result = false;

            if (range.IsValidRange())
                result = range.Contains(comparisonRange.FromDate.Value) && range.Contains(comparisonRange.ToDate.Value);

            return result;
        }

        /// <summary>
        ///Validates if there is overlap of dates between two date ranges.
        ///</summary>
        ///<param name="range">Date range (from - to).</param>
        ///<param name="comparisonRange">Date range to compare.</param>
        ///<returns>Returns a boolean value indicating whether or not there is an overlap between the two date ranges.</returns>
        ///<remarks></remarks>
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

        /// <summary>
        ///Valida si existe superposición de fechas entre dos rangos de fechas.
        ///</summary>
        ///<param name="range">Rango de fechas.</param>
        ///<param name="from">Fechas from a comparar.</param>
        ///<param name="to">Fechas to a comparar.</param>
        ///<returns>Devuelve un valor booleano que indica si existe superposición o no entre ambos rangos de fechas.</returns>
        ///<remarks></remarks>
        public static bool HasOverlap(this IDateRange range, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            DateRange comparisonRange = new DateRange() { FromDate = from, ToDate = to };

            return range.HasOverlap(comparisonRange);
        }

        /// <summary>
        ///Verifica que FromDate y ToDate tenga un valor asignado
        ///</summary>
        ///<param name="range">Representa un range de fechas (Desde y Hasta)</param>
        ///<returns>Devuelve un valor booleano que indica si ambas fechas tienen un valor asignado.</returns>
        ///<remarks></remarks>
        public static bool IsValidRange(this IDateRange range)
        {
            bool result = false;

            result = range.FromDate.HasValue && range.ToDate.HasValue;

            return result;
        }
    }
}
