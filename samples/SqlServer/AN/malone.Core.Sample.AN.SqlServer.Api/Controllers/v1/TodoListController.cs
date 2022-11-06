using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using malone.Core.EF.Entities.Filters;
using malone.Core.Sample.AN.SqlServer.Api.Controllers.v1.Params;
using malone.Core.Sample.AN.SqlServer.Api.ViewModel;
using malone.Core.Sample.AN.SqlServer.Middle.BL;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Services.Requests;
using malone.Core.WebApi;
using Microsoft.Web.Http;

namespace malone.Core.Sample.AN.SqlServer.Api.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [RoutePrefix("v{version:apiVersion}/List")]
    public class TodoListController : ApiController<GetListParam, AddListParam,UpdListParam, TodoList, ITodoListBC>
    {

        public Mapper Mapper { get; set; }

        public TodoListController(ITodoListBC service, Mapper mapperInstance) : base(service)
        {
            Mapper = mapperInstance;
        }


        [HttpPost()]
        [Route("FilterBy")]
        [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public IHttpActionResult FilterBy(GetListParam param)
        {
            IEnumerable list = GetList(param);

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
        public override IHttpActionResult Post([FromBody] AddListParam param)
        {
            return base.Post(param);
        }

        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(UpdListParam param)
        {
            return base.Put(param);
        }

        [ResponseType(typeof(void))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        #endregion

        #region Overridden methods

        protected override IEnumerable GetFiltered(GetListParam param)
        {
            var list = service.Get(new FilterExpression<TodoList>()
            {
                Expression = (x => x.Name.Contains(param.Name))
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
