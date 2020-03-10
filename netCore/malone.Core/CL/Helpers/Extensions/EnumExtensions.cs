using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Helpers.Attributes;

namespace malone.Core.CL.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static List<string> GetStringValues(this Type value)
        {
            List<string> values = new List<string>();
            Array enumValues = Enum.GetValues(value);

            foreach (Enum e in enumValues)
            {
                values.Add(e.GetStringValue());
            }

            return values;
        }

        public static Enum GetEnumFromAttribute(Type enType, string attribute)
        {
            Enum eRet = null;

            Array vals = Enum.GetValues(enType);
            foreach (Enum e in vals)
            {
                string att = e.GetStringValue();
                if (att.Equals(attribute))
                {
                    eRet = e;
                    break; 
                }
            }
            return eRet;
        }

    }
}
