using System;
using System.Linq.Expressions;

namespace malone.Core.EF.Entities.Filters
{
    public class FilterExpression<TFilterEntity> : IFilterExpressionEF<TFilterEntity>
        where TFilterEntity : class
    {
        public Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
