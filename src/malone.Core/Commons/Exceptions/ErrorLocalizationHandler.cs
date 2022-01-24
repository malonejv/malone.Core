//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

using malone.Core.Commons.Localization;
using System.Resources;

namespace malone.Core.Commons.Exceptions
{
                internal class ErrorLocalizationHandler : LocalizationHandler<CoreErrors>, IErrorLocalizationHandler
    {
                                public override ResourceManager ResourceManager => Resources.Exceptions.ResourceManager;
    }
}
