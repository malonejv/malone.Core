//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

using System.Resources;
using malone.Core.Commons.Localization;
using malone.Core.Localization;

namespace malone.Core.Commons.Exceptions
{
    internal class ErrorLocalizationHandler : LocalizationHandler<CoreErrors>, IErrorLocalizationHandler
    {
        public override ResourceManager ResourceManager => Resources.Exceptions.ResourceManager;
    }
}
