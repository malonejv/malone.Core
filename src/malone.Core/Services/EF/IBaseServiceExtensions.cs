using System.Linq;
using System.Reflection;

namespace malone.Core.Services.EF
{
	public static class IBaseServiceExtensions
	{
		/// <summary>
		/// Includes the specified properties.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="service">The service.</param>
		/// <param name="properties">The properties.</param>
		/// <returns></returns>
		public static IBaseService<TEntity> Include<TEntity>(this IBaseService<TEntity> service, params string[] properties)
			where TEntity : class
		{
			var serviceType = service.GetType();
			var includePropertiesField = serviceType.GetField(nameof(BaseService<TEntity>.includeProperties), BindingFlags.NonPublic | BindingFlags.Instance);
			var includeProperties = properties.Aggregate((i, j) => $"{i}, {j}");

			includePropertiesField.SetValue(service, includeProperties);

			return service;
		}
	}
}
