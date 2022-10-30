using System.ComponentModel.DataAnnotations;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;
using malone.Core.WebApi.Params;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL.Requests
{
    public class AddListRequest : IRequest<TodoList>
    {
        public string Name { get; set; }
        public CoreUser User { get; set; }

        public TodoList ToEntity() => new TodoList(Name,User);
    }
}