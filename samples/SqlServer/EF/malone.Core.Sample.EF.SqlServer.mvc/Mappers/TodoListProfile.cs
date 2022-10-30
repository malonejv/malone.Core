using AutoMapper;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Sample.EF.SqlServer.mvc.Models;

namespace malone.Core.Sample.EF.SqlServer.mvc.Mappers
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListViewModel>()
                .ForMember(dest => dest.Pending, opt => opt.MapFrom(src => src.PendingItems(false).Count))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.DoneItems(false).Count))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
