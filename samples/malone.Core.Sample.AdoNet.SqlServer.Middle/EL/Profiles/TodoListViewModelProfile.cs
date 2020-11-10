using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.ViewModel;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Profiles
{
    public class TodoListViewModelProfile : Profile
    {
        public TodoListViewModelProfile()
        {
            IMappingExpression<TodoList, TodoListViewModel> mappingExpression = CreateMap<TodoList, TodoListViewModel>();
        }
    }
}
