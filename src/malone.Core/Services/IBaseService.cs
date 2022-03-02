﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;

namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="T: IBaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IBaseService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		/// <summary>
		/// Gets or sets the ServiceValidator.
		/// </summary>
		TValidator ServiceValidator { get; set; }

		/// <summary>
		/// Gets or sets the Repository.
		/// </summary>
		IBaseRepository<TEntity> Repository { get; set; }

		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> GetAll(
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = ""
);

		/// <summary>
		/// The Get.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="T: TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> Get<TFilter>(
TFilter filter = default(TFilter),
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = "")
where TFilter : class, IFilterExpression;

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="T: TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: TEntity"/>.</returns>
		TEntity GetEntity<TFilter>(
TFilter filter = default(TFilter),
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = "")
where TFilter : class, IFilterExpression;

		/// <summary>
		/// The Add.
		/// </summary>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="T: bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="T: bool"/>.</param>
		void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="oldValues">The oldValues<see cref="T: TEntity"/>.</param>
		/// <param name="newValues">The newValues<see cref="T: TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="T: bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="T: bool"/>.</param>
		void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="T: bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="T: bool"/>.</param>
		void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true);
	}

}