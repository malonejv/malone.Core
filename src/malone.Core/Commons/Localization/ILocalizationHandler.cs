//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:05</date>

using System;

namespace malone.Core.Commons.Localization
{
                    public interface ILocalizationHandler<TCode>
        where TCode : Enum
    {
                                                        string GetString(TCode code, params object[] args);
    }
}
