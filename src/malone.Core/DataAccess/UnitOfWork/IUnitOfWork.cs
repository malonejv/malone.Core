//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:14</date>

using malone.Core.DataAccess.Context;
using System;

namespace malone.Core.DataAccess.UnitOfWork
{
    /// <summary>
    /// Defines the <see cref="IUnitOfWork" />.
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
        /// <returns>The <see cref="int"/>.</returns>
        int SaveChanges();

        /// <summary>
        /// The Dispose.
        /// </summary>
        void Dispose();
    }
}
