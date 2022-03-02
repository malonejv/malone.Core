//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:14</date>

namespace malone.Core.DataAccess.UnitOfWork
{
	using System;
	using malone.Core.DataAccess.Context;

	/// <summary>
	/// Defines the <see cref="T: IUnitOfWork" />.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Gets the Context.
		/// </summary>
		IContext Context { get; }

		/// <summary>
		/// The SaveChanges.
		/// </summary>
		/// <returns>The <see cref="T: int"/>.</returns>
		int SaveChanges();

		/// <summary>
		/// The Dispose.
		/// </summary>
		void Dispose();
	}
}
