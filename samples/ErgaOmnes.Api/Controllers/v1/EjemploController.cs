﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using ErgaOmnes.Core.BL;
using ErgaOmnes.Core.EL.Model;
using ErgaOmnes.Core.EL.RequestParams;
using ErgaOmnes.Core.EL.ViewModel;
using malone.Core.EF.Entities.Filters;
using malone.Core.WebApi;
using Microsoft.Web.Http;

namespace ErgaOmnes.Api.Controllers.v1
{
    /// <inheritdoc cref="CoreApiController{TKey, TParam, TEntity, TBusinessComponent, TBusinessValidator}"/>
    [Authorize]
    [ApiVersion("1.0")]
    [RoutePrefix("v{version:apiVersion}/Ejemplo")]
    public class EjemploController : CoreApiController<EjemploGetRequestParam, Ejemplo, IEjemploBC, IEjemploBV>
    {

        public Mapper Mapper { get; set; }

        /// <summary>
        /// Instancia un controlador de tipo <c>EjemploController</c>.
        /// </summary>
        /// <param name="businessComponent">Componente de negocio para administrar el recurso REST.</param>
        /// <param name="mapperInstance">instancia de Autommaper.</param>
        public EjemploController(IEjemploBC businessComponent, Mapper mapperInstance) : base(businessComponent)
        {
            Mapper = mapperInstance;
        }


        /// <summary>
        /// Obtiene un lista de objetos.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de objetos de tipo <see cref="EjemploViewModel"/>, a partir de los valores de filtrado que se proveen en el parametro <paramref name="parameters"/>.
        /// </remarks>
        /// <response code="200">OK. Devuelve la lista solicitada.</response>
        [HttpPost()]
        [Route("FilterBy")]
        [ResponseType(typeof(IEnumerable<EjemploViewModel>))]
        public IHttpActionResult FilterBy(EjemploGetRequestParam parameters)
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
        /// Si el método <c>AsViewModelList</c> no es sobrescrito los objetos son, por defecto, de tipo <see cref="Ejemplo"/>.
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>
        [ResponseType(typeof(IEnumerable<EjemploViewModel>))]
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
        /// Si el método <c>AsViewModel</c> no es sobrescrito el objeto, por defecto, es de tipo <see cref="Ejemplo"/>.
        /// </remarks>
        /// <param name="id">Id de tipo <c>int</c>.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [ResponseType(typeof(IEnumerable<EjemploViewModel>))]
        public override IHttpActionResult Get(int id)
        {
            return base.Get(id);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Crea un nuevo objeto de tipo <see cref="Ejemplo"/> y lo persiste.
        /// </summary>
        /// <param name="entity">Objeto de tipo <see cref="Ejemplo"/> a crear.</param>
        /// <response code="201">Created. Objeto correctamente creado.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto.</response>
        /// <response code="409">Conflict. El objeto a crear ya existe.</response>
        [ResponseType(typeof(Ejemplo))]
        public override IHttpActionResult Post([FromBody] Ejemplo entity)
        {
            return base.Post(entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Actualiza el objeto.
        /// </summary>
        /// <remarks>
        /// Actualiza el objeto de tipo <see cref="Ejemplo"/> persistido con <paramref name="id"/> que reemplazará los valores con los pasados por <paramref name="entity"/>.
        /// </remarks>
        /// <param name="id">Id del objeto a actualizar</param>
        /// <param name="entity">Objeto de tipo <see cref="Ejemplo"/> a actualizar.</param>
        /// <response code="200">OK. Objeto actualizado correctamente.</response>        
        /// <response code="400">BadRequest. No se ha actualizado el objeto.</response>
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, [FromBody] Ejemplo entity)
        {
            return base.Put(id, entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Elimina el objeto cuyo Id se corresponda con <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// Elimina un objeto de tipo <see cref="Ejemplo"/> por su Id.
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
        protected override IEnumerable GetFiltered(EjemploGetRequestParam parameters)
        {
            var ejemploParams = parameters as EjemploGetRequestParam;
            var list = BusinessComponent.Get(new FilterExpression<Ejemplo>()
            {
                Expression = (x => x.Text.Contains(ejemploParams.Text))
            });

            return list;
        }

        /// <inheritdoc/>
        protected override IEnumerable AsViewModelList(IEnumerable list)
        {
            var castedList = list.Cast<Ejemplo>();
            var mappedList = Mapper.Map<IEnumerable<Ejemplo>, IEnumerable<EjemploViewModel>>(castedList);

            return mappedList.ToList();
        }

        /// <inheritdoc/>
        protected override object AsViewModel(Ejemplo entity)
        {
            var mappedEntity = Mapper.Map<Ejemplo, EjemploViewModel>(entity);
            
            return mappedEntity;
        }

        #endregion

    }
}