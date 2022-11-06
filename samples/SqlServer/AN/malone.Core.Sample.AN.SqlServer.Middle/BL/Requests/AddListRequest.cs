using malone.Core.Identity.AdoNet.SqlServer.Entities;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;

namespace malone.Core.Sample.AN.SqlServer.Middle.BL.Requests
{
    public class AddListRequest : IRequest<TodoList>
    {
        public string Name { get; set; }
        public CoreUser User { get; set; }

        public TodoList ToEntity() => new TodoList(Name,User);
    }
}