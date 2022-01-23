﻿using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class RoleGetRequest : IFilterExpressionAdoNet
    {
        [DbParameter("@Name", Type = SqlDbType.NVarChar, Order = 1, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

    }
}