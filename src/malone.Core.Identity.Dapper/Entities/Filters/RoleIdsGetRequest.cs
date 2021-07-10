using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.Dapper.Entities.Filters
{
    public class RoleIdsGetRequest<TKey> : IFilterExpressionDapper
        where TKey : IEquatable<TKey>
    {
        [Parameter(name: "@Ids", Direction = ParameterDirection.Input)]
        public TKey[] Ids { get; set; }

    }
}
