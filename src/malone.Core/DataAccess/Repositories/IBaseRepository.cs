//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

namespace malone.Core.DataAccess.Repositories
{
	/// <summary>
	/// Defines the <see cref="IBaseRepository{T}" />.
	/// </summary>
	/// <typeparam name="T">.</typeparam>
	public interface IBaseRepository<T> : IBaseQueryRepository<T>, IBaseCUDRepository<T>
		where T : class
	{
	}
}
