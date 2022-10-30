//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:18</date>


//http://lourenco.co.za/blog/2013/07/audit-trails-concurrency-and-soft-deletion-with-entity-framework/

namespace malone.Core.Entities.Model
{
	/// <summary>
	/// Defines the <see cref="IVersionable" />.
	/// </summary>
	public interface IVersionable
	{
		/// <summary>
		/// Gets or sets the Version.
		/// </summary>
		int Version { get; }
	}
}
