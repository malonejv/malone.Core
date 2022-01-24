using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Entities.Model;
using System;
using System.Collections;
using System.Web.Http;

namespace malone.Core.WebApi
{
                                                public abstract class ApiController<TKey, TParam, TEntity, TBusinessComponent, TBusinessValidator> : ApiController
        where TKey : IEquatable<TKey>
        where TParam : class, IGetRequestParam
        where TEntity : class, IBaseEntity<TKey>
        where TBusinessValidator : IBusinessValidator<TKey, TEntity>
        where TBusinessComponent : IBusinessComponent<TKey, TEntity, TBusinessValidator>
    {
        protected TBusinessComponent BusinessComponent { get; set; }

        public ApiController(TBusinessComponent businessComponent)
        {
            BusinessComponent = businessComponent;
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

                                                                                        [HttpGet, HttpHead]
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

                                                                public virtual IHttpActionResult Post([FromBody] TEntity entity)
        {
            BusinessComponent.Add(entity);

            var location = new Uri(Request.RequestUri + entity.Id.ToString());

            return Created(location, entity);
        }

        #endregion

        #region PUT (Update)

                                                                                public virtual IHttpActionResult Put(TKey id, [FromBody] TEntity entity)
        {
            BusinessComponent.Update(entity);
            return Ok();
        }

        #endregion

        #region DELETE (Delete)

                                                                                public virtual IHttpActionResult Delete(TKey id)
        {
            BusinessComponent.Delete(id);
            return Ok();
        }

        #endregion
    }

        public abstract class ApiController<TParam, TEntity, TBusinessComponent, TBusinessValidator>
        : ApiController<int, TParam, TEntity, TBusinessComponent, TBusinessValidator>
       where TParam : class, IGetRequestParam
       where TEntity : class, IBaseEntity
       where TBusinessValidator : IBusinessValidator<TEntity>
       where TBusinessComponent : IBusinessComponent<TEntity, TBusinessValidator>
    {
        public ApiController(TBusinessComponent businessComponent) : base(businessComponent)
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
