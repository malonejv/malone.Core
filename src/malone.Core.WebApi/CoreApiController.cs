using System;
using System.Collections;
using System.Web.Http;
using AutoMapper;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Entities.Model;

namespace malone.Core.WebApi
{
    /// <summary>
    /// Clase con funcionalidad base para API's REST
    /// </summary>
    /// <remarks>
    /// Contiene implementaciones de métodos GET, HEAD, POST, PUT y DELETE.
    /// </remarks>
    /// <typeparam name="TKey">Tipo de dato para campo Id</typeparam>
    /// <typeparam name="TParam">Clase para filtrado</typeparam>
    /// <typeparam name="TEntity">Tipo de entidad del recurso REST</typeparam>
    /// <typeparam name="TBusinessComponent">Componente de negocio para administrar la entidad de tipo <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TBusinessValidator">Componente de negocio que ejecuta validaciones sobre los métodos CRUD de la entidad de tipo <typeparamref name="TEntity"/>.</typeparam>
    public abstract class CoreApiController<TKey, TParam, TEntity, TBusinessComponent, TBusinessValidator> : ApiController
        where TKey : IEquatable<TKey>
        where TParam : class, IGetRequestParam
        where TEntity : class, IBaseEntity<TKey>
        where TBusinessValidator : IBusinessValidator<TKey, TEntity>
        where TBusinessComponent : IBusinessComponent<TKey, TEntity, TBusinessValidator>
    {
        protected TBusinessComponent BusinessComponent { get; set; }
        protected IMapper Mapper { get; set; }

        public CoreApiController(TBusinessComponent businessComponent, IMapper mapperInstance)
        {
            BusinessComponent = businessComponent;
            Mapper = mapperInstance;
        }

        #region GET (GetAll)

        /// <summary>
        /// Obtiene un lista de objetos.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de objetos del tipo retornado por el método <c>AsViewModelList</c>.
        /// Si el método <c>AsViewModelList</c> no es sobrescrito los objetos son, por defecto, de tipo <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>
        public virtual IHttpActionResult Get()
        {
            IEnumerable list = GetList(null);

            return Ok(list);
        }

        /// <summary>
        /// Obtiene una lista de objetos que pueden estar filtrados o no.
        /// </summary>
        /// <remarks>
        /// Si el parámetro <paramref name="parameters"/>, por defecto en null, no se provee la consulta
        /// traerá todos los registros de la entidad <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <param name="parameters">Parámetro de filtrado, por defecto en null.</param>
        /// <returns>Devuelve una lista de objetos</returns>
        protected virtual IEnumerable GetList(TParam parameters = null)
        {
            IEnumerable list = null;

            if (parameters == null)
            {
                list = GetAll();
            }
            else
            {
                list = GetFiltered(parameters);
            }
            return AsViewModelList(list);
        }

        /// <summary>
        /// Obtiene todos los registros de la entidad <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo <typeparamref name="TEntity"/>.</returns>
        protected virtual IEnumerable GetAll()
        {
            return BusinessComponent.GetAll();
        }


        /// <summary>
        /// El propósito de este método es obtener todos los registros de la entidad <typeparamref name="TEntity"/> 
        /// que se correspondan con los valores del filtro <typeparamref name="TParam"/> <paramref name="parameters"/>.
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo <typeparamref name="TEntity"/>.</returns>
        protected virtual IEnumerable GetFiltered(TParam parameters)
        {
            throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
        }

        /// <summary>
        /// Método que se provee para poder transformar el objeto a devolver por el método <c>Get()</c>.
        /// </summary>
        /// <remarks>
        /// Si no se sobrescribe este método el objeto devuelto es de tipo <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <param name="list">Lista que contiene los objetos a transformar.</param>
        /// <returns>Lista con los objetos transformados.</returns>
        protected virtual IEnumerable AsViewModelList(IEnumerable list)
        {
            return list;
        }

        #endregion

        #region GET (GetById)

        /// <summary>
        /// Obtiene un objeto cuyo Id se corresponda con <paramref name="id"/>. 
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su Id del tipo retornado por el método <c>AsViewModel</c>.
        /// Si el método <c>AsViewModel</c> no es sobrescrito el objeto, por defecto, es de tipo <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <param name="id">Id de tipo <typeparamref name="TKey"/>.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet,HttpHead]
        public virtual IHttpActionResult Get(TKey id)
        {
            object entity = GetById(id);

            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }


        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su Id del tipo retornado por el método <c>AsViewModel</c>.
        /// Si el método <c>AsViewModel</c> no es sobrescrito el objeto, por defecto, es de tipo <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <param name="id">Id de tipo <typeparamref name="TKey"/>.</param>
        /// <returns>Devuelve un objeto recuperado por su Id</returns>
        protected virtual object GetById(TKey id)
        {
            TEntity entity = BusinessComponent.GetById(id);
            return AsViewModel(entity);
        }

        /// <summary>
        /// Método que se provee para poder transformar el objeto a devolver por el método <c>Get(<typeparamref name="TKey"/> id)</c>.
        /// </summary>
        /// <remarks>
        /// Si no se sobrescribe este método el objeto devuelto es de tipo <typeparamref name="TEntity"/>.
        /// </remarks>
        /// <param name="list">Lista que contiene los objetos a transformar.</param>
        /// <returns>Lista con los objetos transformados.</returns>
        protected virtual object AsViewModel(TEntity entity)
        {
            return entity;
        }

        #endregion

        #region POST (Add)

        /// <summary>
        /// Crea un nuevo objeto de tipo <typeparamref name="TEntity"/> y lo persiste.
        /// </summary>
        /// <param name="entity">Objeto de tipo <typeparamref name="TEntity"/> a crear.</param>
        /// <response code="201">Created. Objeto correctamente creado.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto.</response>
        /// <response code="409">Conflict. El objeto a crear ya existe.</response>
        public virtual IHttpActionResult Post([FromBody] TEntity entity)
        {
            BusinessComponent.Add(entity);

            var location = new Uri(Request.RequestUri + entity.Id.ToString());

            return Created(location, entity);
        }

        #endregion

        #region PUT (Update)

        /// <summary>
        /// Actualiza el objeto.
        /// </summary>
        /// <remarks>
        /// Actualiza el objeto de tipo <typeparamref name="TEntity"/> persistido con <paramref name="id"/> que reemplazará los valores con los pasados por <paramref name="entity"/>.
        /// </remarks>
        /// <param name="entity">Objeto de tipo <typeparamref name="TEntity"/> a actualizar.</param>
        /// <response code="200">OK. Objeto actualizado correctamente.</response>        
        /// <response code="400">BadRequest. No se ha actualizado el objeto.</response>
        public virtual IHttpActionResult Put(TKey id, [FromBody] TEntity entity)
        {
            BusinessComponent.Update(id, entity);
            return Ok();
        }

        #endregion

        #region DELETE (Delete)

        /// <summary>
        /// Elimina el objeto cuyo Id se corresponda con <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// Elimina un objeto de tipo <typeparamref name="TEntity"/> por su Id.
        /// </remarks>
        /// <param name="id">Id de tipo <typeparamref name="TKey"/>.</param>
        /// <response code="200">OK. Objeto eliminado.</response>        
        /// <response code="400">BadRequest. No se ha eliminado el objeto.</response>
        public virtual IHttpActionResult Delete(TKey id)
        {
            BusinessComponent.Delete(id);
            return Ok();
        }

        #endregion
    }

    /// <inheritdoc/>
    public abstract class CoreApiController<TParam, TEntity, TBusinessComponent, TBusinessValidator>
        : CoreApiController<int, TParam, TEntity, TBusinessComponent, TBusinessValidator>
       where TParam : class, IGetRequestParam
       where TEntity : class, IBaseEntity
       where TBusinessValidator : IBusinessValidator<TEntity>
       where TBusinessComponent : IBusinessComponent<TEntity, TBusinessValidator>
    {
        public CoreApiController(TBusinessComponent businessComponent, IMapper mapperInstance) : base(businessComponent, mapperInstance)
        {
        }

        #region GET (GetAll)

        protected override IEnumerable GetFiltered(TParam parameters)
        {
            throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
        }

        #endregion
    }
}
