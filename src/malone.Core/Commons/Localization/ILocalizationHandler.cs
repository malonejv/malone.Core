using System;

namespace malone.Core.Commons.Localization
{
    public interface ILocalizationHandler<TCode>
        where TCode : Enum
    {
        string GetString(TCode code, params object[] args);
    }
}
