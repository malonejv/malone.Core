﻿//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:00</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;

	/// <summary>
	/// Defines the <see cref="T: TypeExtensions" />.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// The GetDefault.
		/// </summary>
		/// <param name="type">The type<see cref="T: Type"/>.</param>
		/// <returns>The <see cref="T: object"/>.</returns>
		public static object GetDefault(this Type type)
		{
			return type == null || !type.IsValueType || Nullable.GetUnderlyingType(type) != null ? null : Activator.CreateInstance(type);
		}
	}
}
