using AutoMapper;
using malone.Core.Sample.Middle.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.Middle.DAL.Mappers
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
