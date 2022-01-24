using AutoMapper;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.RequestParams;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.ViewModel;
using malone.Core.WebApi;
using Microsoft.Web.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace malone.Core.Sample.AdoNet.SqlServer.Api.Controllers.v1
{
        [Authorize]
    [ApiVersion("1.0")]
    [RoutePrefix("v{version:apiVersion}/List")]
    public class TodoListController : ApiController<TodoListGetRequestParam, TodoList, ITodoListBC, ITodoListBV>
    {

        public Mapper Mapper { get; set; }

                                                public TodoListController(ITodoListBC businessComponent, Mapper mapperInstance) : base(businessComponent)
        {
            Mapper = mapperInstance;
        }


                                                                        [HttpPost()]
        [Route("FilterBy")]
        [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public IHttpActionResult FilterBy(TodoListGetRequestParam parameters)
        {
            IEnumerable list = GetList(parameters);

            return Ok(list);
        }

        #region Overridden HTTP VERBS - Solo se sobrescriben para obtener los comentarios y poder describir los métodos en swagger.

                                                                                [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public override IHttpActionResult Get()
        {
            return base.Get();
        }

                                                                                                [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public override IHttpActionResult Get(int id)
        {
            return base.Get(id);
        }

                                                                        [ResponseType(typeof(TodoList))]
        public override IHttpActionResult Post([FromBody] TodoList entity)
        {
            return base.Post(entity);
        }

                                                                                                [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, [FromBody] TodoList entity)
        {
            return base.Put(id, entity);
        }

                                                                                        [ResponseType(typeof(void))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        #endregion

        #region Overridden methods

                protected override IEnumerable GetFiltered(TodoListGetRequestParam parameters)
        {
            var todoListParams = parameters as TodoListGetRequestParam;
            var list = BusinessComponent.Get(new TodoListGetRequest()
            {
                Name = todoListParams.Name
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

        #endregion

    }
}
