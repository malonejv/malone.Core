﻿using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Entities.Filters;
using System;
using System.Data;

namespace malone.Core.Identity.AdoNet.SqlServer.Entities.Filters
{
    public class UserClaimGetRequest<TKey> : IFilterExpressionAdoNet
        where TKey : IEquatable<TKey>
    {
        //TODO: Modifiar DbParameter para que sea dinamico a partir de TKey
        [DbParameter("@UserId", Type = SqlDbType.Int, Order = 1, Direction = ParameterDirection.Input)]
        public TKey UserId { get; set; }

        [DbParameter("@ClaimType", Type = SqlDbType.NVarChar, Order = 2, Direction = ParameterDirection.Input)]
        public string ClaimType { get; set; }

        [DbParameter("@ClaimValue", Type = SqlDbType.NVarChar, Order = 3, Direction = ParameterDirection.Input)]
        public string ClaimValue { get; set; }

    }
}
