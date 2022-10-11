using System;
using System.Collections;
using System.Web.Http;
using malone.Core.Services;
using malone.Core.Commons.Exceptions;
using malone.Core.Entities.Model;

namespace malone.Core.WebApi
{
    public abstract class ApiController<TKey, TParam, TEntity, TService, TServiceValidator> : ApiController
        where TKey : IEquatable<TKey>
        where TParam : class, IGetRequestParam
        where TEntity : class, IBaseEntity<TKey>
        where TServiceValidator : IServiceValidator<TKey, TEntity>
        where TService : IService<TKey, TEntity, TServiceValidator>
    {
        protected TService Service { get; set; }

        public ApiController(TService businessComponent)
        {
            Service = businessComponent;
        }

        #region GET (GetAll)

        public virtual IHttpActionResult Get()
        {
            IEnumerable list = GetList(null);

            return Ok(list);
        }

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

        protected virtual IEnumerable GetAll()
        {
            return Service.GetAll();
        }


        protected virtual IEnumerable GetFiltered(TParam parameters)
        {
            throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
        }

        protected virtual IEnumerable AsViewModelList(IEnumerable list)
        {
            return list;
        }

        #endregion

        #region GET (GetById)

        [HttpGet, HttpHead]
        public virtual IHttpActionResult Get(TKey id)
        {
            object entity = GetById(id);

            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NotFound();
            }
        }


        protected virtual object GetById(TKey id)
        {
            TEntity entity = Service.GetById(id);
            return AsViewModel(entity);
        }

        protected virtual object AsViewModel(TEntity entity)
        {
            return entity;
        }

        #endregion

        #region POST (Add)

        public virtual IHttpActionResult Post([FromBody] TEntity entity)
        {
            Service.Add(entity);

            var location = new Uri(Request.RequestUri + entity.Id.ToString());

            return Created(location, entity);
        }

        #endregion

        #region PUT (Update)

        public virtual IHttpActionResult Put(TKey id, [FromBody] TEntity entity)
        {
            Service.Update(entity);
            return Ok();
        }

        #endregion

        #region DELETE (Delete)

        public virtual IHttpActionResult Delete(TKey id)
        {
            Service.Delete(id);
            return Ok();
        }

        #endregion
    }

    public abstract class ApiController<TParam, TEntity, TService, TServiceValidator>
    : ApiController<int, TParam, TEntity, TService, TServiceValidator>
   where TParam : class, IGetRequestParam
   where TEntity : class, IBaseEntity
   where TServiceValidator : IServiceValidator<TEntity>
   where TService : IService<TEntity, TServiceValidator>
    {
        public ApiController(TService businessComponent) : base(businessComponent)
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
