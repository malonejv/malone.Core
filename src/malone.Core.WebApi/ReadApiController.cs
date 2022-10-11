﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using malone.Core.Commons.Exceptions;
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
	public abstract class ReadApiController<TKey, TParam, TEntity, TService, TServiceValidator> : ApiController
		where TKey : IEquatable<TKey>
		where TParam : class, IGetRequestParam
		where TEntity : class, IBaseEntity<TKey>
		where TServiceValidator : IServiceValidator<TKey, TEntity>
		where TService : IQueryService<TKey, TEntity, TServiceValidator>
	{
		/// <summary>
		/// Gets or sets the Service.
		/// </summary>
		protected TService Service { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadApiController{TKey, TParam, TEntity, TService, TServiceValidator}"/> class.
		/// </summary>
		/// <param name="businessComponent">The businessComponent<see cref="TService"/>.</param>
		public ReadApiController(TService businessComponent)
		{
			Service = businessComponent;
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
		/// <param name="parameters">The parameters<see cref="TParam"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable GetList(TParam parameters = null)
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
			return Service.GetAll();
		}

		/// <summary>
		/// The GetFiltered.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="TParam"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected virtual IEnumerable GetFiltered(TParam parameters)
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
			TEntity entity = Service.GetById(id);
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
	/// Defines the <see cref="ReadApiController{TParam, TEntity, TService, TServiceValidator}" />.
	/// </summary>
	/// <typeparam name="TParam">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TService">.</typeparam>
	/// <typeparam name="TServiceValidator">.</typeparam>
	public abstract class ReadApiController<TParam, TEntity, TService, TServiceValidator>
		: ReadApiController<int, TParam, TEntity, TService, TServiceValidator>
		where TParam : class, IGetRequestParam
		where TEntity : class, IBaseEntity
		where TServiceValidator : IServiceValidator<TEntity>
		where TService : IQueryService<TEntity, TServiceValidator>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadApiController{TParam, TEntity, TService, TServiceValidator}"/> class.
		/// </summary>
		/// <param name="businessComponent">The businessComponent<see cref="TService"/>.</param>
		public ReadApiController(TService businessComponent) : base(businessComponent)
		{
		}

		/// <summary>
		/// The GetFiltered.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="TParam"/>.</param>
		/// <returns>The <see cref="IEnumerable"/>.</returns>
		protected override IEnumerable GetFiltered(TParam parameters)
		{
			throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
		}
	}
}