using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DataAccess.Repositories
{
	public interface IBaseDataOperationsRepository<T>
		where T : class
	{
		/// <summary>
		/// The Insert.
		/// </summary>
		/// <param name="entity">The entity<see cref="T"/>.</param>
		void Insert(T entity);

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="entity">The entity<see cref="T"/>.</param>
		void Delete(T entity);


		//TODO: Revisar. Eliminar esta implementación.

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="oldValues">The oldValues<see cref="T"/>.</param>
		/// <param name="newValues">The newValues<see cref="T"/>.</param>
		void Update(T oldValues, T newValues);
	}
}
