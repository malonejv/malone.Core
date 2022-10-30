namespace malone.Core.WebApi
{
	using System;
	using System.Collections;
	using System.Web.Http;
	using malone.Core.Commons.Exceptions;
	using malone.Core.Entities.Model;
	using malone.Core.Services;
	using malone.Core.Services.Requests;
	using malone.Core.WebApi.Params;

	/// <summary>Defines the <see cref="ApiController{TKey, TFilter, TEntity, TService}" />.</summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TFilter">Type used for filter parameters.</typeparam>
	/// <typeparam name="TEntity">Entity type.</typeparam>
	/// <typeparam name="TService">Service type.</typeparam>

	public abstract class ApiController<TKey, TFilter, TAdd, TUpdate, TEntity, TService> : ApiController
		where TKey : IEquatable<TKey>
		where TFilter : class, IParam
		where TAdd : class, IAddParam<TKey, TEntity>
		where TUpdate : class, IUpdParam<TKey, TEntity>
		where TEntity : class, IBaseEntity<TKey>
		where TService : IService<TKey, TEntity>
	{
		/// <summary>
		/// Gets or sets the Service.
		/// </summary>
		protected TService service;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiController{TKey, TFilter, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public ApiController(TService service)
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
		/// <param name="parameters">The parameters <see cref="TFilter"/>.</param>
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
		/// <param name="parameters">The parameters <see cref="TFilter"/>.</param>
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
		/// <param name="id">The id <see cref="TKey"/>.</param>
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
		/// <param name="id">The id <see cref="TKey"/>.</param>
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

		/// <summary>
		/// The Post.
		/// </summary>
		/// <param name="param">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Post([FromBody] TAdd param)
		{
			var entity = param.ToEntity();
			var id = service.Add(entity);

			var location = new Uri(Request.RequestUri + id.ToString());

			return Created(location, entity);
		}

		/// <summary>
		/// The Put.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <param name="param">The request <see cref="IRequest{TEntity}"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Put(TUpdate param)
		{
			var entity = service.GetById(param.Id);
			service.Update(param.ToEntity(entity));
			return Ok();
		}

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Delete(TKey id)
		{
			service.Delete(id);
			return Ok();
		}
	}

	/// <summary>
	/// Defines the <see cref="FullApiController{TFilter, TEntity, TService}" />.
	/// </summary>
	/// <typeparam name="TFilter">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>

	public abstract class ApiController<TFilter, TAdd, TUpdate, TEntity, TService> :
		ApiController<int, TFilter, TAdd, TUpdate, TEntity, TService>
		where TFilter : class, IParam
		where TAdd : class, IAddParam<TEntity>
		where TUpdate : class, IUpdParam<TEntity>
		where TEntity : class, IBaseEntity
		where TService : IService<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FullApiController{TFilter, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public ApiController(TService service) : base(service)
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
