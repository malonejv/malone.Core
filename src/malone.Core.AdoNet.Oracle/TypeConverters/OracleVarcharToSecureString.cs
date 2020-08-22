﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.AdoNet.Parameters;
using malone.Core.Commons.Helpers.Extensions;

namespace malone.Core.AdoNet.Oracle.TypeConverters
{
    public class OracleVarcharToSecureString : IParameterConverter
    {
        public object Convert(object value)
        {
            return value != null ? (object)value.ToString().ToSecureString() : (object)null;
        }
    }
}
