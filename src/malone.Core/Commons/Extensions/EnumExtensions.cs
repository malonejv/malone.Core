//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:58</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace malone.Core.Commons.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            DescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Description : null;
        }

        public static List<string> GetDescriptions(this Type value)
        {
            List<string> values = new List<string>();
            Array enumValues = Enum.GetValues(value);

            foreach (Enum e in enumValues)
            {
                values.Add(e.GetDescription());
            }

            return values;
        }

        public static Enum GetEnumFromAttribute(Type enType, string attribute)
        {
            Enum eRet = null;

            Array vals = Enum.GetValues(enType);
            foreach (Enum e in vals)
            {
                string att = e.GetDescription();
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
