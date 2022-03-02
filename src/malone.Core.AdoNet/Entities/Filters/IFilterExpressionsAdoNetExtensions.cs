namespace malone.Core.AdoNet.Entities.Filters
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Reflection;
	using malone.Core.AdoNet.Attributes;
	using malone.Core.AdoNet.Database;
	using malone.Core.Commons.Helpers.Extensions;

	/// <summary>
	/// Defines the <see cref="IFilterExpressionAdoNetExtensions" />.
	/// </summary>
	public static class IFilterExpressionAdoNetExtensions
	{
		/// <summary>
		/// The GetParameters.
		/// </summary>
		/// <param name="filter">The filter<see cref="IFilterExpressionAdoNet"/>.</param>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <returns>The <see cref="IEnumerable{DbParameterWithValue}"/>.</returns>
		public static IEnumerable<DbParameterWithValue> GetParameters(this IFilterExpressionAdoNet filter, IDbCommand command)
		{
			var properties = filter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				// Get the stringvalue attributes
				DbParameterAttribute dbParameterInfo = propertyInfo.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;
				object parameterValue = propertyInfo.GetValue(filter) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(filter) : DBNull.Value;
				yield return new DbParameterWithValue
				{
					DbParameter = dbParameterInfo,
					Value = parameterValue
				};
			}
		}
	}
}
