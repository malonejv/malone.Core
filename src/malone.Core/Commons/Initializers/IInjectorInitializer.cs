//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

namespace malone.Core.Commons.Initializers
{
	/// <summary>
	/// Defines the <see cref="T: IInjectorInitializer{TContainer}" />.
	/// </summary>
	/// <typeparam name="TContainer">.</typeparam>
	public interface IInjectorInitializer<TContainer>
	{
		/// <summary>
		/// The Initialize.
		/// </summary>
		/// <returns>The <see cref="T: TContainer"/>.</returns>
		TContainer Initialize();

		/// <summary>
		/// The Terminate.
		/// </summary>
		void Terminate();
	}
}
