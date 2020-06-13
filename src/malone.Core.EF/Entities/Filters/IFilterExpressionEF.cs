using malone.Core.Entities.Filters;
using System;
using System.Linq.Expressions;

namespace malone.Core.EF.Entities.Filters
{
    public interface IFilterExpressionEF<TFilterEntity> : IFilterExpression
        where TFilterEntity : class
    {
        Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
