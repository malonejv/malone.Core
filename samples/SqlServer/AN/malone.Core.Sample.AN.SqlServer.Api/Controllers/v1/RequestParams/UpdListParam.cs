using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;
using malone.Core.WebApi.Params;

namespace malone.Core.Sample.AN.SqlServer.Api.Controllers.v1.Params
{
    public class UpdListParam : IUpdParam<TodoList>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TodoList ToEntity(TodoList entity)
        {
            entity.UpdateName(Name);

            return entity;
        }
    }
}