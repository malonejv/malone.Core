using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Filters;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.RequestParams;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.ViewModel;
using malone.Core.WebApi;
using Microsoft.Web.Http;

namespace malone.Core.Sample.AdoNet.SqlServer.Api.Controllers.v1
{
    /// <inheritdoc cref="CoreApiController{TKey, TParam, TEntity, TBusinessComponent, TBusinessValidator}"/>
    [Authorize]
    [ApiVersion("1.0")]
    [RoutePrefix("v{version:apiVersion}/List")]
    public class TodoListController : CoreApiController<TodoListGetRequestParam, TodoList, ITodoListBC, ITodoListBV>
    {

        public Mapper Mapper { get; set; }

        /// <summary>
        /// Instancia un controlador de tipo <c>TodoListController</c>.
        /// </summary>
        /// <param name="businessComponent">Componente de negocio para administrar el recurso REST.</param>
        /// <param name="mapperInstance">instancia de Autommaper.</param>
        public TodoListController(ITodoListBC businessComponent, Mapper mapperInstance) : base(businessComponent)
        {
            Mapper = mapperInstance;
        }


        /// <summary>
        /// Obtiene un lista de objetos.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de objetos de tipo <see cref="TodoListViewModel"/>, a partir de los valores de filtrado que se proveen en el parametro <paramref name="parameters"/>.
        /// </remarks>
        /// <response code="200">OK. Devuelve la lista solicitada.</response>
        [HttpPost()]
        [Route("FilterBy")]
        [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public IHttpActionResult FilterBy(TodoListGetRequestParam parameters)
        {
            IEnumerable list = GetList(parameters);
            
            return Ok(list);
        }

        #region Overridden HTTP VERBS - Solo se sobrescriben para obtener los comentarios y poder describir los métodos en swagger.

        /// <inheritdoc />
        /// <summary>
        /// Obtiene un lista de objetos.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de objetos del tipo retornado por el método <c>AsViewModelList</c>.
        /// Si el método <c>AsViewModelList</c> no es sobrescrito los objetos son, por defecto, de tipo <see cref="TodoList"/>.
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>
        [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public override IHttpActionResult Get()
        {
            return base.Get();
        }

        /// <inheritdoc/>
        /// <summary>
        /// Obtiene un objeto cuyo Id se corresponda con <paramref name="id"/>. 
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su Id del tipo retornado por el método <c>AsViewModel</c>.
        /// Si el método <c>AsViewModel</c> no es sobrescrito el objeto, por defecto, es de tipo <see cref="TodoList"/>.
        /// </remarks>
        /// <param name="id">Id de tipo <c>int</c>.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [ResponseType(typeof(IEnumerable<TodoListViewModel>))]
        public override IHttpActionResult Get(int id)
        {
            return base.Get(id);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Crea un nuevo objeto de tipo <see cref="TodoList"/> y lo persiste.
        /// </summary>
        /// <param name="entity">Objeto de tipo <see cref="TodoList"/> a crear.</param>
        /// <response code="201">Created. Objeto correctamente creado.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto.</response>
        /// <response code="409">Conflict. El objeto a crear ya existe.</response>
        [ResponseType(typeof(TodoList))]
        public override IHttpActionResult Post([FromBody] TodoList entity)
        {
            return base.Post(entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Actualiza el objeto.
        /// </summary>
        /// <remarks>
        /// Actualiza el objeto de tipo <see cref="TodoList"/> persistido con <paramref name="id"/> que reemplazará los valores con los pasados por <paramref name="entity"/>.
        /// </remarks>
        /// <param name="id">Id del objeto a actualizar</param>
        /// <param name="entity">Objeto de tipo <see cref="TodoList"/> a actualizar.</param>
        /// <response code="200">OK. Objeto actualizado correctamente.</response>        
        /// <response code="400">BadRequest. No se ha actualizado el objeto.</response>
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, [FromBody] TodoList entity)
        {
            return base.Put(id, entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Elimina el objeto cuyo Id se corresponda con <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// Elimina un objeto de tipo <see cref="TodoList"/> por su Id.
        /// </remarks>
        /// <param name="id">Id de tipo <c>int</c>.</param>
        /// <response code="200">OK. Objeto eliminado.</response>        
        /// <response code="400">BadRequest. No se ha eliminado el objeto.</response>
        [ResponseType(typeof(void))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        #endregion

        #region Overridden methods

        /// <inheritdoc/>
        protected override IEnumerable GetFiltered(TodoListGetRequestParam parameters)
        {
            var todoListParams = parameters as TodoListGetRequestParam;
            var list = BusinessComponent.Get(new TodoListGetRequest()
            {
                Name = todoListParams.Name
            });

            return list;
        }

        /// <inheritdoc/>
        protected override IEnumerable AsViewModelList(IEnumerable list)
        {
            var castedList = list.Cast<TodoList>();
            var mappedList = Mapper.Map<IEnumerable<TodoList>, IEnumerable<TodoListViewModel>>(castedList);

            return mappedList.ToList();
        }

        /// <inheritdoc/>
        protected override object AsViewModel(TodoList entity)
        {
            var mappedEntity = Mapper.Map<TodoList, TodoListViewModel>(entity);
            
            return mappedEntity;
        }

        #endregion

    }
}
