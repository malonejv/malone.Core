﻿using malone.Core.WebApi;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.RequestParams
{
    public class TodoListGetRequestParam : IGetRequestParam
    {
        public string Name { get; set; }
    }
}
