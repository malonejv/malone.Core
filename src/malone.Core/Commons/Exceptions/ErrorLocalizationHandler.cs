//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

namespace malone.Core.Commons.Exceptions
{
	using System.Resources;
	using malone.Core.Localization;

	/// <summary>
	/// Defines the <see cref="T: ErrorLocalizationHandler" />.
	/// </summary>
	internal class ErrorLocalizationHandler : LocalizationHandler<CoreErrors>, IErrorLocalizationHandler
	{
		/// <summary>
		/// Gets the ResourceManager.
		/// </summary>
		public override ResourceManager ResourceManager => Resources.Exceptions.ResourceManager;
	}
}
