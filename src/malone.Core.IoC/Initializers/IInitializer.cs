//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

namespace malone.Core.IoC.Initializers
{
    public interface IInitializer<TContainer>
    {
        void Initialize(TContainer container);
    }
}
