//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:20</date>

namespace malone.Core.Resources.Admin
{
	/// <summary>
	/// Defines the <see cref="IResourceAdmin" />.
	/// </summary>
	public interface IResourceAdmin
	{
		/// <summary>
		/// The GetText.
		/// </summary>
		/// <param name="clave">The clave<see cref="string"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		string GetText(string clave);

		/// <summary>
		/// The GetText.
		/// </summary>
		/// <param name="clave">The clave<see cref="string"/>.</param>
		/// <param name="parametros">The parametros<see cref="T: string[]"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		string GetText(string clave, params string[] parametros);

		/// <summary>
		/// The GetResource.
		/// </summary>
		/// <param name="clave">The clave<see cref="string"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		string GetResource(string clave);
	}
}
