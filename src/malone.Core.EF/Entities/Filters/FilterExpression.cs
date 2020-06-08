using malone.Core.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EF.Entities.Filters
{
    public class FilterExpression<TFilterEntity> : IFilterExpressionEF<TFilterEntity>
        where TFilterEntity : class
    {
        public Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
