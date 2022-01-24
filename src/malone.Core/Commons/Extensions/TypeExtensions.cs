//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:00</date>

using System;

namespace malone.Core.Commons.Helpers.Extensions
{
                public static class TypeExtensions
    {
                                                public static object GetDefault(this Type type)
        {
            return type == (Type)null || !type.IsValueType || Nullable.GetUnderlyingType(type) != (Type)null ? (object)null : Activator.CreateInstance(type);
        }
    }
}
