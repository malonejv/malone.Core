﻿//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

namespace malone.Core.DataAccess.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using malone.Core.Entities.Filters;

	/// <summary>
	/// Defines the <see cref="T: IBaseRepository{T}" />.
	/// </summary>
	/// <typeparam name="T">.</typeparam>
	public interface IBaseRepository<T> : IBaseQueryOperationsRepository<T>, IBaseDataOperationsRepository<T>
		where T : class
	{
	}
}
