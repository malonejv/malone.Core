//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:18</date>

namespace malone.Core.Entities.Model
{
	/// <summary>
	/// Defines the <see cref="ISoftDelete" />.
	/// </summary>
	public interface ISoftDelete
	{
		/// <summary>
		/// Gets or sets a value indicating whether IsDeleted.
		/// </summary>
		bool IsDeleted { get; set; }
	}
}
