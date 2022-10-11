using System;
using System.Web.Http;
using malone.Core.Entities.Model;
using malone.Core.Services;

namespace malone.Core.WebApi
{
	/// <summary>
	/// Defines the <see cref="FullApiController{TKey, TParam, TEntity, TService, TServiceValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TParam">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	/// <typeparam name="TServiceValidator">.</typeparam>
	public abstract class WriteApiController<TKey, TParam, TEntity, TService, TServiceValidator> : ApiController
		where TKey : IEquatable<TKey>
		where TParam : class, IGetRequestParam
		where TEntity : class, IBaseEntity<TKey>
		where TServiceValidator : IServiceValidator<TKey, TEntity>
		where TService : ICUDService<TKey, TEntity, TServiceValidator>
	{
		/// <summary>
		/// Gets or sets the Service.
		/// </summary>
		protected TService Service { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WriteApiController{TKey, TParam, TEntity, TService, TServiceValidator}"/> class.
		/// </summary>
		/// <param name="businessComponent">The businessComponent<see cref="TService"/>.</param>
		public WriteApiController(TService businessComponent)
		{
			Service = businessComponent;
		}

		/// <summary>
		/// The Post.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Post([FromBody] TEntity entity)
		{
			Service.Add(entity);

			var location = new Uri(Request.RequestUri + entity.Id.ToString());

			return Created(location, entity);
		}

		/// <summary>
		/// The Put.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Put(TKey id, [FromBody] TEntity entity)
		{
			Service.Update(entity);
			return Ok();
		}

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <returns>The <see cref="IHttpActionResult"/>.</returns>
		public virtual IHttpActionResult Delete(TKey id)
		{
			Service.Delete(id);
			return Ok();
		}
	}


	/// <summary>
	/// Defines the <see cref="WriteApiController{TParam, TEntity, TService, TServiceValidator}" />.
	/// </summary>
	/// <typeparam name="TParam">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	/// <typeparam name="TServiceValidator">.</typeparam>
	public abstract class WriteApiController<TParam, TEntity, TService, TServiceValidator>
		: WriteApiController<int, TParam, TEntity, TService, TServiceValidator>
		where TParam : class, IGetRequestParam
		where TEntity : class, IBaseEntity
		where TServiceValidator : IServiceValidator<TEntity>
		where TService : ICUDService<TEntity, TServiceValidator>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WriteApiController{TParam, TEntity, TService, TServiceValidator}"/> class.
		/// </summary>
		/// <param name="businessComponent">The businessComponent<see cref="TService"/>.</param>
		public WriteApiController(TService businessComponent) : base(businessComponent)
		{
		}

	}
}
