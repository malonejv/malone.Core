using malone.Core.Commons.Localization;
using System.Resources;

namespace malone.Core.Sample.EF.Firebird.Middle.CL.Exceptions
{
    public class ErrorLocalizationHandler : LocalizationHandler<ErrorCode>, IErrorLocalizationHandler
    {
        public override ResourceManager ResourceManager => Exceptions.ResourceManager;
    }
}
