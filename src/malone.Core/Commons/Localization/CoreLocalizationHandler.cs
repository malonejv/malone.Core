//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:04</date>

namespace malone.Core.Commons.Localization
{
	using System.Resources;
	using malone.Core.Localization;

	/// <summary>
	/// Defines the <see cref="T: ContentLocalizationHandler" />.
	/// </summary>
	internal class ContentLocalizationHandler : LocalizationHandler<CoreContents>, IContentLocalizationHandler
	{
		/// <summary>
		/// Gets the ResourceManager.
		/// </summary>
		public override ResourceManager ResourceManager => Resources.Contents.ResourceManager;
	}
}
