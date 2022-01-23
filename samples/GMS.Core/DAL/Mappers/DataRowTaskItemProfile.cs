using AutoMapper;
using GMS.Core.EL.Model;
using System.Data;

namespace GMS.Core.DAL.Mappers
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
