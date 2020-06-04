using malone.Core.BL.Components.Interfaces;
using malone.Core.EL.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace malone.Core.WebApi
{
    public abstract class CoreApiController<TKey, TEntity, TBusinessComponent, TBusinessValidator, TErrorCoder> : ApiController
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TBusinessValidator : IBusinessValidator<TKey, TEntity, TErrorCoder>
        where TBusinessComponent : IBusinessComponent<TKey, TEntity, TBusinessValidator, TErrorCoder>
        where TErrorCoder : Enum
    {
        private TBusinessComponent BusinessComponent { get; set; }

        public CoreApiController(TBusinessComponent businessComponent)
        {
            BusinessComponent = businessComponent;
        }

        // GET api/ejemplo
        public virtual HttpResponseMessage Get()
        {
            try
            {
                var list = BusinessComponent.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "mensaje");
            }
        }

        // GET api/ejemplo/5
        public virtual HttpResponseMessage Get(TKey id)
        {
            try
            {
                TEntity entity = BusinessComponent.GetById(id);

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

        // POST api/ejemplo
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

        // PUT api/ejemplo/5
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

        // DELETE api/ejemplo/5
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
    }

    public abstract class CoreApiController<TEntity, TBusinessComponent, TBusinessValidator, TErrorCoder>
        : CoreApiController<int, TEntity, TBusinessComponent, TBusinessValidator, TErrorCoder>
       where TEntity : class, IBaseEntity
       where TBusinessValidator : IBusinessValidator<TEntity, TErrorCoder>
       where TBusinessComponent : IBusinessComponent<TEntity, TBusinessValidator, TErrorCoder>
       where TErrorCoder : Enum
    {
        public CoreApiController(TBusinessComponent businessComponent) : base(businessComponent)
        {
        }
    }
}
