//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

namespace malone.Core.Commons.Initializers
{
    /// <summary>
    /// Defines the <see cref="IInitializer{TContainer}" />.
    /// </summary>
    /// <typeparam name="TContainer">.</typeparam>
    public interface IInitializer<TContainer>
    {
        /// <summary>
        /// The Initialize.
        /// </summary>
        /// <param name="container">The container<see cref="TContainer"/>.</param>
        void Initialize(TContainer container);
    }
}
