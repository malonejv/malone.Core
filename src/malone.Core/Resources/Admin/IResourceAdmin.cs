//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:20</date>

namespace malone.Core.Resources.Admin
{
                public interface IResourceAdmin
    {
                                                string GetText(string clave);

                                                        string GetText(string clave, params string[] parametros);

                                                string GetResource(string clave);
    }
}
