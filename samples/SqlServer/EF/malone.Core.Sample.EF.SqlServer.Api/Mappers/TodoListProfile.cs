using AutoMapper;
using malone.Core.Sample.EF.SqlServer.Api.ViewModel;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Api.Mappers
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            IMappingExpression<TodoList, TodoListViewModel> mappingExpression = CreateMap<TodoList, TodoListViewModel>();
        }
    }
}
