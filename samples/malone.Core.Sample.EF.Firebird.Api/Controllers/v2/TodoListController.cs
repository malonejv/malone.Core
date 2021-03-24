using AutoMapper;
using malone.Core.EF.Entities.Filters;
using malone.Core.Sample.EF.Firebird.Middle.BL;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;
using malone.Core.Sample.EF.Firebird.Middle.EL.RequestParams;
using malone.Core.Sample.EF.Firebird.Middle.EL.ViewModel;
using malone.Core.WebApi;
using Microsoft.Web.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace malone.Core.Sample.EF.Firebird.Api.Controllers.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    [RoutePrefix("v{version:apiVersion}/List")]
    public class TodoListController : ApiController<TodoListGetRequestParam, TodoList, ITodoListBC, ITodoListBV>
    {
        public Mapper Mapper { get; set; }

        public TodoListController(ITodoListBC businessComponent, Mapper mapperInstance) : base(businessComponent)
        {
        }

        //GET entity
        [HttpPost()]
        //[Route("~/TodoList/FilterBy")]
        [Route("FilterBy")]
        public IHttpActionResult FilterBy(TodoListGetRequestParam parameters)
        {
            IEnumerable list = GetList(parameters);

            return Ok(list);
        }



        protected override IEnumerable GetFiltered(TodoListGetRequestParam parameters)
        {
            var todoListParams = parameters as TodoListGetRequestParam;
            var list = BusinessComponent.Get(new FilterExpression<TodoList>()
            {
                Expression = (x => x.Name.Contains(todoListParams.Name))
            });

            return list;
        }

        protected override IEnumerable AsViewModelList(IEnumerable list)
        {
            var castedList = list.Cast<TodoList>();
            var mappedList = Mapper.Map<IEnumerable<TodoList>, IEnumerable<TodoListViewModel>>(castedList);

            return mappedList.ToList();
        }

        protected override object AsViewModel(TodoList entity)
        {
            var mappedEntity = Mapper.Map<TodoList, TodoListViewModel>(entity);

            return mappedEntity;
        }
    }
}
