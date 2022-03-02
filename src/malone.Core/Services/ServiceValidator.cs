//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:40</date>

namespace malone.Core.Services
{
	using System;
	using System.Collections.Generic;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="ServiceValidator{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public class ServiceValidator<TKey, TEntity> : BaseServiceValidator<TEntity>, IServiceValidator<TKey, TEntity>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceValidator{TKey, TEntity}"/> class.
		/// </summary>
		public ServiceValidator() : base()
		{
		}
	}

	/// <summary>
	/// Defines the <see cref="ServiceValidator{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public class ServiceValidator<TEntity> : ServiceValidator<int, TEntity>, IServiceValidator<TEntity>
where TEntity : class, IBaseEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceValidator{TEntity}"/> class.
		/// </summary>
		public ServiceValidator() : base()
		{
		}
	}
}
