namespace malone.Core.AdoNet.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Reflection;
	using malone.Core.AdoNet.Attributes;
	using malone.Core.AdoNet.Database;
	using malone.Core.Commons.Helpers.Extensions;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IBaseEntityAdoNetExtensions" />.
	/// </summary>
	public static class IBaseEntityAdoNetExtensions
	{
		/// <summary>
		/// Defines the ID.
		/// </summary>
		private static readonly string ID = "Id";

		/// <summary>
		/// The GetNotKeyParameters.
		/// </summary>
		/// <typeparam name="TKey">Type used for key property.</typeparam>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="IEnumerable{DbParameterWithValue}"/>.</returns>
		public static IEnumerable<DbParameterWithValue> GetNotKeyParameters<TKey, TEntity>(this TEntity entity)
			where TKey : IEquatable<TKey>
			where TEntity : class, IBaseEntity<TKey>
		{
			var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				if (propertyInfo.Name != ID)
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

		/// <summary>
		/// The GetKeyParameter.
		/// </summary>
		/// <typeparam name="TKey">Type used for key property.</typeparam>
		/// <param name="entityType">The entityType<see cref="Type"/>.</param>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <returns>The <see cref="DbParameterWithValue"/>.</returns>
		public static DbParameterWithValue GetKeyParameter<TKey>(this Type entityType, TKey id) where TKey : IEquatable<TKey>
		{
			if (typeof(IBaseEntity<TKey>).IsAssignableFrom(entityType))
			{
				var interfaceType = entityType.GetInterface(typeof(IBaseEntity<TKey>).Name);

				var targetMethods = from method in entityType.GetInterfaceMap(interfaceType).TargetMethods
									select method;

				var propertyInfoId = (from prop in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
									  where (targetMethods.Contains(prop.GetGetMethod(true)) || targetMethods.Contains(prop.GetSetMethod(true)))
										 && prop.Name == ID
									  select prop).Single();

				DbParameterAttribute dbParameterInfo = propertyInfoId.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;

				// Get the stringvalue attributes
				//DbParameterIdAttribute dbParameterInfo = entityType.GetCustomAttribute(typeof(DbParameterIdAttribute), false) as DbParameterIdAttribute;
				return new DbParameterWithValue
				{
					DbParameter = dbParameterInfo,
					Value = id
				};
			}
			//TODO: manejar con excepciones de core.
			throw new InvalidOperationException("Se esperaba IBaseEntity<TKey>");
		}
	}
}
