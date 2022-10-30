using System.ComponentModel.DataAnnotations;
using malone.Core.Sample.EF.SqlServer.Middle.BL.Requests;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;
using malone.Core.WebApi.Params;

namespace malone.Core.Sample.EF.SqlServer.Api.Controllers.v2.Params
{
    public class AddListParam : IAddParam<TodoList>
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(100)]
        public string Name { get; set; }

        public TodoList ToEntity() => new TodoList(Name);
    }
}