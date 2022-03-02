//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:03</date>

namespace malone.Core.IoC.Initializers
{
    public interface IModuleInitializer<TContainer> : IInitializer<TContainer>
    {
        string Name { get; }
    }
}
