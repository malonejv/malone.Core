//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

namespace malone.Core.Commons.Initializers
{
                    public interface IInjectorInitializer<TContainer>
    {
                                        TContainer Initialize();

                                void Terminate();
    }
}
