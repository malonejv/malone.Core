﻿using System;
using System.Data;
using malone.Core.AdoNet.Attributes;

namespace malone.Core.AdoNet.Oracle.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class OracleResultSetAttribute : DbParameterAttribute
    {
        public OracleResultSetAttribute(string name) : base(name)
        {
            base.Direction = ParameterDirection.Output;
            //base.Type = 121;
        }
    }
}
