using AutoMapper;
using malone.Core.Sample.Middle.EL.Model;
using System.Data;

namespace malone.Core.Sample.Middle.DAL.Mappers
{
    public class DataRowTodoListProfile : Profile
    {
        public DataRowTodoListProfile()
        {
            IMappingExpression<DataRow, TodoList> mappingExpression = CreateMap<DataRow, TodoList>();

            mappingExpression.ForMember(i => i.Id, o => o.MapFrom(s => s["Id"]));
            mappingExpression.ForMember(i => i.Name, o => o.MapFrom(s => s["Name"]));
            mappingExpression.ForMember(i => i.IsDeleted, o => o.MapFrom(s => s["IsDeleted"]));
        }
    }
}
