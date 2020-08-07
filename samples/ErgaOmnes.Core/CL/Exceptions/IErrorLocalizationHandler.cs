using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Localization;

namespace ErgaOmnes.Core.CL.Exceptions
{
    public interface IErrorLocalizationHandler : ILocalizationHandler<ErrorCode>
    {
    }
}
