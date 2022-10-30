using System;
using System.Collections;
using System.Web.Http;
using malone.Core.Commons.Exceptions;
using malone.Core.Entities.Model;
using malone.Core.Services;
using malone.Core.WebApi.Params;

namespace malone.Core.WebApi
{
	/// <summary>
	/// Defines the <see cref="ApiController{TKey, TParam, TEntity, TService}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TFilter">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	public abstract class ReadApiController<TKey, TFilter, TEntity, TService> : ApiController
		where TKey : IEquatable<TKey>
		where TFilter : class, IParam
		where TEntity : class, IBaseEntity<TKey>
		where TService : IService<TKey, TEntity>
	{
		/// <summary>
		/// Gets or sets the Service.
		/// </summary>
		protected TService service;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadApiController{TKey, TParam, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public ReadApiController(TService service)
		{
			this.service = service;
		}

		/// <summary>
		/// The Get.
		/// </summary>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Get()
		{
			IEnumerable list = GetList(null);

			return Ok(list);
		}

		/// <summary>
		/// The GetList.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="TFilter"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable GetList(TFilter parameters = null)
		{
			IEnumerable list = null;

			if (parameters == null)
			{
				list = GetAll();
			}
			else
			{
				list = GetFiltered(parameters);
			}
			return AsViewModelList(list);
		}

		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable GetAll()
		{
			return service.GetAll();
		}

		/// <summary>
		/// The GetFiltered.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="TFilter"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable GetFiltered(TFilter parameters)
		{
			throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
		}

		/// <summary>
		/// The AsViewModelList.
		/// </summary>
		/// <param name="list">The list<see cref="IEnumerable"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable AsViewModelList(IEnumerable list)
		{
			return list;
		}

		/// <summary>
		/// The Get.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		[HttpGet, HttpHead]
		public virtual IHttpActionResult Get(TKey id)
		{
			object entity = GetById(id);

			if (entity != null)
			{
				return Ok(entity);
			}
			else
			{
				return NotFound();
			}
		}

		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <returns>The <see cref="object"/>.</returns>
		protected virtual object GetById(TKey id)
		{
			TEntity entity = service.GetById(id);
			return AsViewModel(entity);
		}

		/// <summary>
		/// The AsViewModel.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="object"/>.</returns>
		protected virtual object AsViewModel(TEntity entity)
		{
			return entity;
		}

	}

	/// <summary>
	/// Defines the <see cref="ReadApiController{TParam, TEntity, TService}" />.
	/// </summary>
	/// <typeparam name="TFilter">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	public abstract class ReadApiController<TFilter, TEntity, TService>
		: ReadApiController<int, TFilter, TEntity, TService>
		where TFilter : class, IParam
		where TEntity : class, IBaseEntity
		where TService : IService<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadApiController{TParam, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public ReadApiController(TService service) : base(service)
		{
		}

		/// <summary>
		/// The GetFiltered.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="TFilter"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected override IEnumerable GetFiltered(TFilter parameters)
		{
			throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
		}
	}
}
