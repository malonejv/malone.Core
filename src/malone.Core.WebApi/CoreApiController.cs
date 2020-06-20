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
        protected Mapper Mapper { get; set; }

        public CoreApiController(TBusinessComponent businessComponent, Mapper mapperInstance)
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
            var list = BusinessComponent.GetAll();
            return list;
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

            var entity = BusinessComponent.GetById(id);
            if (entity != null)
                return entity;
            else
                return null;
        }

        #endregion

        #region POST (Add)

        // POST api/entity
        public virtual IHttpActionResult Post([FromBody]TEntity entity)
        {
            BusinessComponent.Add(entity);

            var location = new Uri(Request.RequestUri + entity.Id.ToString());

            return Created(location,entity);
        }

        #endregion

        #region PUT (Update)

        // PUT api/entity/5
        public virtual IHttpActionResult Put(TKey id, [FromBody]TEntity entity)
        {
                var entityFound = BusinessComponent.GetById(id);

                if (entityFound != null)
                {
                    BusinessComponent.Update(entity);
                    return Ok();
                }
                else
                    return NotFound();
        }

        #endregion

        #region DELETE (Delete)

        // DELETE api/entity/5
        public virtual IHttpActionResult Delete(TKey id)
        {
                var entityFound = BusinessComponent.GetById(id);

                if (entityFound != null)
                {
                    BusinessComponent.Delete(entityFound);
                    return Ok();
                }
                else
                    return NotFound();
        }

        #endregion
    }

    public abstract class CoreApiController<TEntity, TBusinessComponent, TBusinessValidator>
        : CoreApiController<int, TEntity, TBusinessComponent, TBusinessValidator>
       where TEntity : class, IBaseEntity
       where TBusinessValidator : IBusinessValidator<TEntity>
       where TBusinessComponent : IBusinessComponent<TEntity, TBusinessValidator>
    {
        public CoreApiController(TBusinessComponent businessComponent, Mapper mapperInstance) : base(businessComponent, mapperInstance)
        {
        }
    }
}
