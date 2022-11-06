using System;
using System.Linq;
using System.Reflection;
using malone.Core.Entities.Model;

namespace malone.Core.Services.EF
{
	public static class IServiceExtensions
	{
		/// <summary>
		/// Includes the specified properties.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="service">The service.</param>
		/// <param name="properties">The properties.</param>
		/// <returns></returns>
		public static IService<TKey, TEntity> Include<TKey, TEntity>(this IService<TKey, TEntity> service, params string[] properties)
			where TKey : IEquatable<TKey>
			where TEntity : class, IBaseEntity<TKey>
		{
			var serviceType = service.GetType();
			var includePropertiesField = serviceType.GetField(nameof(BaseService<TEntity>.includeProperties), BindingFlags.NonPublic | BindingFlags.Instance);
			var includeProperties = properties.Aggregate((i, j) => $"{i}, {j}");

			includePropertiesField.SetValue(service, includeProperties);

			return service;
		}
	}
}
