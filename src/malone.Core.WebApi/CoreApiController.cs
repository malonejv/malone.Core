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
        public virtual HttpResponseMessage Get()
        {
            try
            {
                var list = GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "mensaje");
            }
        }
        protected virtual IEnumerable GetAll()
        {
            var list = BusinessComponent.GetAll();
            return list;
        }
        #endregion

        #region GET (GetById)

        // GET api/entity/5
        public virtual HttpResponseMessage Get(TKey id)
        {
            try
            {
                object entity = GetById(id);

                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
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
        public virtual HttpResponseMessage Post([FromBody]TEntity entity)
        {
            try
            {
                BusinessComponent.Add(entity);

                var message = Request.CreateResponse(HttpStatusCode.Created, entity);
                message.Headers.Location = new Uri(Request.RequestUri + entity.Id.ToString());
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region PUT (Update)

        // PUT api/entity/5
        public virtual HttpResponseMessage Put(TKey id, [FromBody]TEntity entity)
        {
            try
            {
                var entityFound = BusinessComponent.GetById(id);

                if (entityFound != null)
                {
                    BusinessComponent.Update(entity);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region DELETE (Delete)

        // DELETE api/entity/5
        public virtual HttpResponseMessage Delete(TKey id)
        {
            try
            {
                var entityFound = BusinessComponent.GetById(id);

                if (entityFound != null)
                {
                    BusinessComponent.Delete(entityFound);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
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
