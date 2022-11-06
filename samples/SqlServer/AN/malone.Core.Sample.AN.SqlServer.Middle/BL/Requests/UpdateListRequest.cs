using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;

namespace malone.Core.Sample.AN.SqlServer.Middle.BL.Requests
{
    public class UpdateListRequest : IRequest<TodoList>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TodoList ToEntity()=>new TodoList();
    }
}