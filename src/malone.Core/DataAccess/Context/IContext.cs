//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:12</date>

namespace malone.Core.DataAccess.Context
{
	/// <summary>
	/// Defines the <see cref="T: IContext" />.
	/// </summary>
	public interface IContext
	{
		/// <summary>
		/// The SaveChanges.
		/// </summary>
		/// <returns>The <see cref="T: int"/>.</returns>
		int SaveChanges();

		/// <summary>
		/// The Dispose.
		/// </summary>
		void Dispose();
	}
}
