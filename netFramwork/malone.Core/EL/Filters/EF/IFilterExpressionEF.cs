using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EL.Filters
{
    public interface IFilterExpressionEF<TFilterEntity> : IFilterExpression
        where TFilterEntity : class
    {
        Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
