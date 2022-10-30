//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:16</date>

//http://lourenco.co.za/blog/2013/07/audit-trails-concurrency-and-soft-deletion-with-entity-framework/
namespace malone.Core.Entities.Model
{
	using System;

	/// <summary>
	/// Defines the <see cref="IAuditable" />.
	/// </summary>
	public interface IAuditable
	{
		/// <summary>
		/// Gets or sets the CreatedById.
		/// </summary>
		Guid CreatedById { get; }

		/// <summary>
		/// Gets or sets the CreatedDateTime.
		/// </summary>
		DateTime CreatedDateTime { get; }

		/// <summary>
		/// Gets or sets the EditedById.
		/// </summary>
		Guid EditedById { get; }

		/// <summary>
		/// Gets or sets the EditedDateTime.
		/// </summary>
		DateTime EditedDateTime { get; }
	}
}
