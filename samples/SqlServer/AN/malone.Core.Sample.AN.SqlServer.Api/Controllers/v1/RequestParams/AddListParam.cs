using System.ComponentModel.DataAnnotations;
using malone.Core.Identity.AdoNet.Entities;
using malone.Core.Sample.AN.SqlServer.Middle.BL.Requests;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;
using malone.Core.WebApi.Params;

namespace malone.Core.Sample.AN.SqlServer.Api.Controllers.v1.Params
{
    public class AddListParam : IAddParam<TodoList>
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(100)]
        public string Name { get; set; }

        CoreUser User { get; set; }

        public TodoList ToEntity()=>new TodoList(Name, User);
    }
}