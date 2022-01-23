﻿using malone.Core.AdoNet.Parameters;
using malone.Core.Commons.Helpers.Extensions;

namespace malone.Core.AdoNet.Oracle.TypeConverters
{
    public class FbVarcharToSecureString : IParameterConverter
    {
        public object Convert(object value)
        {
            return value != null ? (object)value.ToString().ToSecureString() : (object)null;
        }
    }
}