//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:52</date>

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    internal enum CoreErrors
    {
        /// <summary>
        /// Ocurrió un error al tratar de obtener el nombre de seccion del tipo {0}.
        /// </summary>
        CONF1 = 1,
        /// <summary>
        /// No se encuentra configurada la sección para el tipo {0}.
        /// </summary>
        CONF2 = 2,
        /// <summary>
        /// Perdón, algo salió mal.\n{0}: {1}
        /// </summary>
        TECH200 = 200,
        /// <summary>
        /// Se produjo un error inesperado.
        /// </summary>
        TECH201 = 201,
        /// <summary>
        /// El método {0} de la clase {1} no se encuentra implementado.
        /// </summary>
        TECH202 = 202,
        ///// <summary>
        ///// No se encontró un mensaje en el recurso para {0}.{1}.
        ///// </summary>
        //TECH3 = 3,

        ///// <summary>
        ///// No se pudo obtener una instancia de ResourceManager.
        ///// </summary>
        //TECH4 = 4,



        /// <summary>
        /// Usuario no autorizado.
        /// </summary>
        SERVICE300 = 300,
        /// <summary>
        /// Se produjo un error al intentar obtener los datos solicitados.
        /// </summary>
        BUSINESS400 = 400,
        /// <summary>
        /// Se produjo un error al intentar guardar los datos solicitados.
        /// </summary>
        BUSINESS401 = 401,
        /// <summary>
        /// Se produjo un error al intentar eliminar el dato solicitado.
        /// </summary>
        BUSINESS402 = 402,
        /// <summary>
        /// Se produjo un error al intentar actualizar los datos solicitados.
        /// </summary>
        BUSINESS403 = 403,
        /// <summary>
        /// No se encontraron resultados para la consulta realizada.
        /// </summary>
        BUSINESS404 = 404,
        /// <summary>
        /// Nombre de usuario o contraseña invalido.
        /// </summary>
        BUSINESS405 = 405,
        /// <summary>
        /// Debes confirmar el email primero.
        /// </summary>
        BUSINESS406 = 406,
        /// <summary>
        /// La consulta para obtener una entidad de tipo {0}, por id {1} no devolvió resultados.
        /// </summary>
        BUSVAL500 = 500,
        /// <summary>
        /// Error al obtener una lista ordenada de tipo {0}.
        /// </summary>
        DATAACCESS600 = 600,
        /// <summary>
        /// Error al obtener una entidad de tipo {0}.
        /// </summary>
        DATAACCESS601 = 601,
        /// <summary>
        /// Error al insertar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS602 = 602,
        /// <summary>
        /// Error al eliminar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS603 = 603,
        /// <summary>
        /// Error al actualizar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS604 = 604,
        /// <summary>
        /// Error en la validación de CommandText {0}.
        /// </summary>
        DATAACCESS605 = 605,
        /// <summary>
        /// Se produjo un error en la llamada al servicio {0}.
        /// </summary>
        SERVAG700 = 700
    }
}
