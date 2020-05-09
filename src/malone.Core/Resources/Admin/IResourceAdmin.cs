using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Resources.Admin
{
    public interface IResourceAdmin
    {
        /// <summary>
        /// Obtiene un recurso de texto, el cual es fijo (no es parametrizable).
        /// </summary>
        /// <param name="clave">Clave del recurso de texto a obtener.</param>
        /// <returns>String que representa el recurso de texto pedido.</returns>
        /// <remarks></remarks>
        string GetText(string clave);

        /// <summary>
        /// Obtiene un mensaje que posee parámetros variables.
        /// </summary>
        /// <param name="clave">Clave del recurso de texto a obtener.</param>
        /// <param name="parametros">Parámetros del mensaje.</param>
        /// <returns>String que representa el recurso de texto completo, con los parámtros reemplazados.</returns>
        string GetText(string clave, params string[] parametros);

        /// <summary>
        ///Obtiene un recurso de texto, que representa una ruta de una imagen. el cual es fijo (no es parametrizable).
        ///</summary>
        ///<param name="clave">Clave del recurso del path a obtener.</param>
        ///<returns>String que representa el path pedido.</returns>
        ///<remarks></remarks>
        string GetResource(string clave);
    }
}
