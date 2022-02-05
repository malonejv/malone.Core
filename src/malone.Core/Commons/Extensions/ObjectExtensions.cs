//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:59</date>

using System;

namespace malone.Core.Commons.Helpers.Extensions
{

    internal static class ObjectExtensions
    {
        internal static bool IsNull<T>(this T @object)
        {
            return @object == null;
        }

        internal static bool IsNotNull<T>(this T @object)
        {
            return @object != null;
        }

        internal static bool IsDefault<T>(this T @object)
        {
            return @object.Equals(default(T));
        }

        internal static void ThrowIfNull<T>(this T @object, string paramName)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
            }
        }
    }
}
