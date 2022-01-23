using malone.Core.WebApi;

namespace malone.Core.Sample.EF.Firebird.Middle.EL.RequestParams
{
    public class TodoListGetRequestParam : IGetRequestParam
    {
        public string Name { get; set; }
    }
}
