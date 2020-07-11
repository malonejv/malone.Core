using AutoMapper;
using malone.Core.Business.Components;
using malone.Core.Entities.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace malone.Core.WebApi
{
    public abstract class CoreApiController<TKey, TEntity, TBusinessComponent, TBusinessValidator> : ApiController
        where TKey : IEquatable<TKey>
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
        // GET api/entity
        public virtual IHttpActionResult Get()
        {
            var list = GetAll();
            return Ok(list);
        }
        protected virtual IEnumerable GetAll()
        {
            return BusinessComponent.GetAll();
        }
        #endregion

        #region GET (GetById)

        // GET api/entity/5
        public virtual IHttpActionResult Get(TKey id)
        {
            object entity = GetById(id);

            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }

        protected virtual object GetById(TKey id)
        {
            return BusinessComponent.GetById(id);
        }

        #endregion

        #region POST (Add)

        // POST api/entity
        public virtual IHttpActionResult Post([FromBody] TEntity entity)
        {
            BusinessComponent.Add(entity);

            var location = new Uri(Request.RequestUri + entity.Id.ToString());

            return Created(location, entity);
        }

        #endregion

        #region PUT (Update)

        // PUT api/entity/5
        public virtual IHttpActionResult Put(TKey id, [FromBody] TEntity entity)
        {
            BusinessComponent.Update(id, entity);
            return Ok();
        }

        #endregion

        #region DELETE (Delete)

        // DELETE api/entity/5
        public virtual IHttpActionResult Delete(TKey id)
        {
            BusinessComponent.Delete(id);
            return Ok();
        }

        #endregion
    }

    public abstract class CoreApiController<TEntity, TBusinessComponent, TBusinessValidator>
        : CoreApiController<int, TEntity, TBusinessComponent, TBusinessValidator>
       where TEntity : class, IBaseEntity
       where TBusinessValidator : IBusinessValidator<TEntity>
       where TBusinessComponent : IBusinessComponent<TEntity, TBusinessValidator>
    {
        public CoreApiController(TBusinessComponent businessComponent, IMapper mapperInstance) : base(businessComponent, mapperInstance)
        {
        }
    }
}
