using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EL.Filters
{
    public interface IFilterEF<TFilterEntity> : IFilter
        where TFilterEntity : class, IBaseEntity
    {
        Expression<Func<TFilterEntity, bool>> Expression { get; set; }
    }
}
