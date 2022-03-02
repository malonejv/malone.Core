//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:58</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;

	/// <summary>
	/// Defines the <see cref="T: EnumExtensions" />.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// The GetDescription.
		/// </summary>
		/// <param name="value">The value<see cref="T: Enum"/>.</param>
		/// <returns>The <see cref="T: string"/>.</returns>
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

		/// <summary>
		/// The GetDescriptions.
		/// </summary>
		/// <param name="value">The value<see cref="T: Type"/>.</param>
		/// <returns>The <see cref="T: List{string}"/>.</returns>
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

		/// <summary>
		/// The GetEnumFromAttribute.
		/// </summary>
		/// <param name="enType">The enType<see cref="T: Type"/>.</param>
		/// <param name="attribute">The attribute<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: Enum"/>.</returns>
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
