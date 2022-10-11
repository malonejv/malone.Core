using System;
using System.Linq.Expressions;
using malone.Core.Entities.Filters;

namespace malone.Core.EF.Entities.Filters
{
	public interface IFilterExpressionEF<TFilterEntity> : IFilterExpression
		where TFilterEntity : class
	{
		Expression<Func<TFilterEntity, bool>> Expression { get; set; }
	}
}
