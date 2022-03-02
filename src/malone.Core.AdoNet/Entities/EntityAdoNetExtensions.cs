namespace malone.Core.AdoNet.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using malone.Core.AdoNet.Attributes;
	using malone.Core.AdoNet.Database;
	using malone.Core.Commons.Helpers.Extensions;

	/// <summary>
	/// Defines the <see cref="T: EntityAdoNetExtensions" />.
	/// </summary>
	public static class EntityAdoNetExtensions
	{
		/// <summary>
		/// The GetParameters.
		/// </summary>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <returns>The <see cref="T: IEnumerable{DbParameterWithValue}"/>.</returns>
		public static IEnumerable<DbParameterWithValue> GetParameters<TEntity>(this TEntity entity)
			where TEntity : class
		{
			var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				// Get the stringvalue attributes
				DbParameterAttribute dbParameterInfo = propertyInfo.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;
				if (dbParameterInfo != null)
				{
					object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
					yield return new DbParameterWithValue
					{
						DbParameter = dbParameterInfo,
						Value = parameterValue
					};
				}
			}
		}
	}
}
