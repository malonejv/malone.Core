//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:03</date>

namespace malone.Core.Commons.Initializers
{
	using System;
	using System.Collections.Generic;
	using malone.Core.Commons.DI;

	/// <summary>
	/// Defines the <see cref="LayersInitializer{TContainer}" />.
	/// </summary>
	/// <typeparam name="TContainer">.</typeparam>
	public abstract class LayersInitializer<TContainer> : ILayerInitializer<TContainer>
	{
		/// <summary>
		/// Gets the Layers.
		/// </summary>
		public abstract IEnumerable<IInitializer<TContainer>> Layers { get; }

		/// <summary>
		/// The Initialize.
		/// </summary>
		/// <param name="container">The container<see cref="TContainer"/>.</param>
		public void Initialize(TContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(nameof(container));
			}

			foreach (var layer in Layers)
			{
				layer.Initialize(container);
			}
		}
	}
}
