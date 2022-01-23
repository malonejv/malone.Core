﻿using AutoMapper;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.ViewModel;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Profiles
{
    public class TodoListViewModelProfile : Profile
    {
        public TodoListViewModelProfile()
        {
            IMappingExpression<TodoList, TodoListViewModel> mappingExpression = CreateMap<TodoList, TodoListViewModel>();
        }
    }
}
