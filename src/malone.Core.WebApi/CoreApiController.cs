using AutoMapper;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
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

        // GET api/entity
        //[HttpGet()]
        //public virtual IHttpActionResult GetFilterBy([FromUri] IGetRequestParam parameters = null)
        //{
        //    IEnumerable list = GetList(parameters);

        //    return Ok(list);
        //}


        // GET api/entity
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
            return BusinessComponent.GetAll();
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
            TEntity entity = BusinessComponent.GetById(id);
            return AsViewModel(entity);
        }

        protected virtual object AsViewModel(TEntity entity)
        {
            return entity;
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

        //// GET api/entity
        //public override IHttpActionResult GetFilterBy([FromUri] IGetRequestParam parameters = null)
        //{
        //    return base.GetFilterBy(parameters);
        //}

        protected override IEnumerable GetList(TParam parameters = null)
        {
            return base.GetList(parameters);
        }

        protected override IEnumerable GetFiltered(TParam parameters)
        {
            throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.TECH202, "GetFiltered", this.GetType().Name);
        }

        #endregion
    }
}
