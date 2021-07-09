//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:00</date>

using System;

namespace malone.Core.Commons.Helpers.Extensions
{
    /// <summary>
    /// Defines the <see cref="TypeExtensions" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// The GetDefault.
        /// </summary>
        /// <param name="type">The type<see cref="Type"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static object GetDefault(this Type type)
        {
            return type == (Type)null || !type.IsValueType || Nullable.GetUnderlyingType(type) != (Type)null ? (object)null : Activator.CreateInstance(type);
        }
    }
}
