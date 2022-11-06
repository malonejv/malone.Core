using System;
using System.Web.Http;
using malone.Core.Entities.Model;
using malone.Core.Services;
using malone.Core.WebApi.Params;

namespace malone.Core.WebApi
{
	/// <summary>
	/// Defines the <see cref="ApiController{TKey,  TAdd, TUpdate, TEntity, TService}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TParam">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	public abstract class WriteApiController<TKey, TAdd, TUpdate, TEntity, TService> : ApiController
		where TKey : IEquatable<TKey>
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
		/// Initializes a new instance of the <see cref="WriteApiController{TKey, TAdd, TUpdate, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public WriteApiController(TService service)
		{
			this.service = service;
		}

		/// <summary>
		/// The Post.
		/// </summary>
		/// <param name="param">The request <typeparamref name="TAdd"/>.</param>
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
		/// <param name="param">The request <typeparamref name="TUpdate"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Put([FromBody] TUpdate param)
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
	/// Defines the <see cref="WriteApiController{TParam, TEntity, TService}" />.
	/// </summary>
	/// <typeparam name="TParam">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	public abstract class WriteApiController<TAdd, TUpdate, TEntity, TService>
		: WriteApiController<int, TAdd, TUpdate, TEntity, TService>
		where TAdd : class, IAddParam<TEntity>
		where TUpdate : class, IUpdParam<TEntity>
		where TEntity : class, IBaseEntity
		where TService : IService<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WriteApiController{TParam, TEntity, TService}"/> class.
		/// </summary>
		/// <param name="service">The service<see cref="TService"/>.</param>
		public WriteApiController(TService service) : base(service)
		{
		}

	}
}
