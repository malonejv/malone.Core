//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

using malone.Core.Commons.Localization;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Defines the <see cref="IErrorLocalizationHandler" />.
    /// </summary>
    internal interface IErrorLocalizationHandler : ILocalizationHandler<CoreErrors>
    {
    }
}
