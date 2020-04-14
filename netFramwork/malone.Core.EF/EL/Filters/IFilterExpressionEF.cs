using malone.Core.EL.Filters;
using System;
using System.Linq.Expressions;

namespace malone.Core.EF.EL.Filters
{
    public interface IFilterExpressionEF<TFilterEntity> : IFilterExpression
        where TFilterEntity : class
    {
        Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
