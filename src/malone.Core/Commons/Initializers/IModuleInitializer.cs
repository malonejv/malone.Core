//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:03</date>

namespace malone.Core.Commons.Initializers
{
	/// <summary>
	/// Defines the <see cref="T: IModuleInitializer{TContainer}" />.
	/// </summary>
	/// <typeparam name="TContainer">.</typeparam>
	public interface IModuleInitializer<TContainer> : IInitializer<TContainer>
	{
		/// <summary>
		/// Gets the Name.
		/// </summary>
		string Name { get; }
	}
}
