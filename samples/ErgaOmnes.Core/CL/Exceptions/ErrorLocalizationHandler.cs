using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Localization;

namespace ErgaOmnes.Core.CL.Exceptions
{
    public class ErrorLocalizationHandler : LocalizationHandler<ErrorCode>, IErrorLocalizationHandler
    {
        public override ResourceManager ResourceManager => Exceptions.ResourceManager;
    }
}
