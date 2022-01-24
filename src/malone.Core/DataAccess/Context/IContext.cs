//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:12</date>

namespace malone.Core.DataAccess.Context
{
                public interface IContext
    {
                                        int SaveChanges();

                                void Dispose();
    }
}
