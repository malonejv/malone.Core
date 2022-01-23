using AutoMapper;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using System.Data;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Mappers
{
    public class DataRowTaskItemProfile : Profile
    {
        public DataRowTaskItemProfile()
        {
            IMappingExpression<DataRow, TaskItem> mappingExpression = CreateMap<DataRow, TaskItem>();

            mappingExpression.ForMember(i => i.Id, o => o.MapFrom(s => s["Id"]));
            mappingExpression.ForMember(i => i.Description, o => o.MapFrom(s => s["Description"]));
            mappingExpression.ForMember(i => i.IsDeleted, o => o.MapFrom(s => s["IsDeleted"]));
        }
    }
}
