//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

namespace malone.Core.Commons.DI
{
	using System.Collections.Generic;
	using malone.Core.Commons.Initializers;

	/// <summary>
	/// Defines the <see cref="ILayerInitializer{TContainer}" />.
	/// </summary>
	/// <typeparam name="TContainer">.</typeparam>
	public interface ILayerInitializer<TContainer> : IInitializer<TContainer>
	{
		/// <summary>
		/// Gets the Layers.
		/// </summary>
		IEnumerable<IInitializer<TContainer>> Layers { get; }
	}
}
