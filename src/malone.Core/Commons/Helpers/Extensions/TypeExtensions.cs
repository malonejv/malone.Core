using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Helpers.Extensions
{
    public static class TypeExtensions
    {
        //https://stackoverflow.com/questions/325426/programmatic-equivalent-of-defaulttype
        public static object GetDefault(this Type type)
        {
            return type == (Type)null || !type.IsValueType || Nullable.GetUnderlyingType(type) != (Type)null ? (object)null : Activator.CreateInstance(type);
        }
    }
}
