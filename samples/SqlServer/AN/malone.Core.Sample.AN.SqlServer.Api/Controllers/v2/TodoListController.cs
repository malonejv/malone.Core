using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using malone.Core.EF.Entities.Filters;
using malone.Core.Sample.AN.SqlServer.Api.Controllers.v2.Params;
using malone.Core.Sample.AN.SqlServer.Api.ViewModel;
using malone.Core.Sample.AN.SqlServer.Middle.BL;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.WebApi;
using Microsoft.Web.Http;

namespace malone.Core.Sample.AN.SqlServer.Api.Controllers.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    [RoutePrefix("v{version:apiVersion}/List")]
    public class TodoListController : ApiController<GetListParam, AddListParam, UpdListParam, TodoList, ITodoListBC>
    {
        public Mapper mapper;

        public TodoListController(ITodoListBC service, Mapper mapper) : base(service)
        {
            this.mapper = mapper;
        }

        //GET entity
        [HttpPost()]
        //[Route("~/TodoList/FilterBy")]
        [Route("FilterBy")]
        public IHttpActionResult FilterBy(GetListParam parameters)
        {
            IEnumerable list = GetList(parameters);

            return Ok(list);
        }



        protected override IEnumerable GetFiltered(GetListParam parameters)
        {
            var todoListParams = parameters;
            var list = service.Get(new FilterExpression<TodoList>()
            {
                Expression = (x => x.Name.Contains(todoListParams.Name))
            });

            return list;
        }

        protected override IEnumerable AsViewModelList(IEnumerable list)
        {
            var castedList = list.Cast<TodoList>();
            var mappedList = mapper.Map<IEnumerable<TodoList>, IEnumerable<TodoListViewModel>>(castedList);

            return mappedList.ToList();
        }

        protected override object AsViewModel(TodoList entity)
        {
            var mappedEntity = mapper.Map<TodoList, TodoListViewModel>(entity);

            return mappedEntity;
        }
    }
}
