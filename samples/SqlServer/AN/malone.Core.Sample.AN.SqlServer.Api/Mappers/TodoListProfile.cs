using AutoMapper;
using malone.Core.Sample.AN.SqlServer.Api.ViewModel;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AN.SqlServer.Api.Mappers
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            IMappingExpression<TodoList, TodoListViewModel> mappingExpression = CreateMap<TodoList, TodoListViewModel>();
        }
    }
}
