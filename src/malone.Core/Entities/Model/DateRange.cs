//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:16</date>

namespace malone.Core.Entities.Model
{
	using System;

	/// <summary>
	/// Defines the <see cref="T: DateRange" />.
	/// </summary>
	public class DateRange : IDateRange
	{
		/// <summary>
		/// Gets or sets the FromDate.
		/// </summary>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets the ToDate.
		/// </summary>
		public DateTime? ToDate { get; set; }
	}
}
