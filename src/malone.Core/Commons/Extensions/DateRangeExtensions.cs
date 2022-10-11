//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:58</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="DateRangeExtensions" />.
	/// </summary>
	public static class DateRangeExtensions
	{
		/// <summary>
		/// The Contains.
		/// </summary>
		/// <param name="range">The range<see cref="IDateRange"/>.</param>
		/// <param name="date">The date<see cref="DateTime"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public static bool Contains(this IDateRange range, DateTime date)
		{
			bool result = false;

			if (range.IsValidRange())
			{
				result = range.FromDate <= date && range.ToDate >= date;
			}

			return result;
		}

		/// <summary>
		/// The Contains.
		/// </summary>
		/// <param name="range">The range<see cref="IDateRange"/>.</param>
		/// <param name="comparisonRange">The comparisonRange<see cref="IDateRange"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public static bool Contains(this IDateRange range, IDateRange comparisonRange)
		{
			bool result = false;

			if (range.IsValidRange())
			{
				result = range.Contains(comparisonRange.FromDate.Value) && range.Contains(comparisonRange.ToDate.Value);
			}

			return result;
		}

		/// <summary>
		/// The HasOverlap.
		/// </summary>
		/// <param name="range">The range<see cref="IDateRange"/>.</param>
		/// <param name="comparisonRange">The comparisonRange<see cref="IDateRange"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public static bool HasOverlap(this IDateRange range, IDateRange comparisonRange)
		{
			bool result = false;

			if (range.IsValidRange() & comparisonRange.IsValidRange())
			{
				result = range.Contains(comparisonRange.FromDate.Value)
					   || range.Contains(comparisonRange.ToDate.Value)
					   || range.Contains(comparisonRange)
					   || comparisonRange.Contains(range);
			}

			return result;
		}

		/// <summary>
		/// The HasOverlap.
		/// </summary>
		/// <param name="range">The range<see cref="IDateRange"/>.</param>
		/// <param name="from">The from<see cref="Nullable{DateTime}"/>.</param>
		/// <param name="to">The to<see cref="Nullable{DateTime}"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public static bool HasOverlap(this IDateRange range, Nullable<DateTime> from, Nullable<DateTime> to)
		{
			DateRange comparisonRange = new DateRange() { FromDate = from, ToDate = to };

			return range.HasOverlap(comparisonRange);
		}

		/// <summary>
		/// The IsValidRange.
		/// </summary>
		/// <param name="range">The range<see cref="IDateRange"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public static bool IsValidRange(this IDateRange range)
		{
			bool result = false;

			result = range.FromDate.HasValue && range.ToDate.HasValue;

			return result;
		}
	}
}
