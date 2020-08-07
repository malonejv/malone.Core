using System.Resources;
using malone.Core.Commons.Localization;

namespace malone.Core.Commons.Exceptions
{
    internal class ErrorLocalizationHandler : LocalizationHandler<CoreErrors>, IErrorLocalizationHandler
    {
        public override ResourceManager ResourceManager => Resources.Exceptions.ResourceManager;
    }
}
