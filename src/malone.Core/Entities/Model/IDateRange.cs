//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:18</date>

namespace malone.Core.Entities.Model
{
	using System;

	/// <summary>
	/// Defines the <see cref="IDateRange" />.
	/// </summary>
	public interface IDateRange
	{
		/// <summary>
		/// Gets or sets the FromDate.
		/// </summary>
		DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets the ToDate.
		/// </summary>
		DateTime? ToDate { get; set; }
	}
}
