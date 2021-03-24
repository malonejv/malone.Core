//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:17</date>

using System;

namespace malone.Core.Entities.Model
{
    /// <summary>
    /// Defines the <see cref="IBaseEntity{TKey}" />.
    /// </summary>
    /// <typeparam name="TKey">.</typeparam>
    public interface IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="IBaseEntity" />.
    /// </summary>
    public interface IBaseEntity : IBaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        new int Id { get; set; }
    }
}
