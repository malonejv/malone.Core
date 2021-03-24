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
        /// Obtiene un recurso de texto, el cual es fijo (no es parametrizable).
        /// </summary>
        /// <param name="clave">Clave del recurso de texto a obtener.</param>
        /// <returns>String que representa el recurso de texto pedido.</returns>
        string GetText(string clave);

        /// <summary>
        /// Obtiene un mensaje que posee parámetros variables.
        /// </summary>
        /// <param name="clave">Clave del recurso de texto a obtener.</param>
        /// <param name="parametros">Parámetros del mensaje.</param>
        /// <returns>String que representa el recurso de texto completo, con los parámtros reemplazados.</returns>
        string GetText(string clave, params string[] parametros);

        /// <summary>
        /// Obtiene un recurso de texto, que representa una ruta de una imagen. el cual es fijo (no es parametrizable).
        /// </summary>
        /// <param name="clave">The clave<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetResource(string clave);
    }
}
