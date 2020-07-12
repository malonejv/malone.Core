using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ErgaOmnes.Core.BL;
using ErgaOmnes.Core.EL.Model;
using ErgaOmnes.Core.EL.RequestParams;
using ErgaOmnes.Core.EL.ViewModel;
using malone.Core.EF.Entities.Filters;
using malone.Core.WebApi;

namespace ErgaOmnes.Api.Controllers
{
    //[Authorize]
    public class EjemploController : CoreApiController<Ejemplo, IEjemploBC, IEjemploBV>
    {
        public EjemploController(IEjemploBC businessComponent, Mapper mapperInstance) : base(businessComponent, mapperInstance)
        {
        }


        protected override IEnumerable GetFiltered(IGetRequestParam<Ejemplo> parameters)
        {
            var ejemploParams = parameters as EjemploGetRequestParams;
            var list = BusinessComponent.Get(new FilterExpression<Ejemplo>()
            {
                Expression = (x => x.Text.Contains(ejemploParams.Text))
            });

            return list;
        }

        protected override IEnumerable AsViewModelList(IEnumerable list)
        {
            var castedList = list.Cast<Ejemplo>();
            var mappedList = Mapper.Map<IEnumerable<Ejemplo>, IEnumerable<EjemploViewModel>>(castedList);

            return mappedList.ToList();
        }

        protected override object AsViewModel(Ejemplo entity)
        {
            var mappedEntity = Mapper.Map<Ejemplo, EjemploViewModel>(entity);

            return mappedEntity;
        }

        //private IEjemploBC EjemploBC { get; set; }

        //public EjemploController(IEjemploBC ejemploBC)
        //{
        //    EjemploBC = ejemploBC;
        //}

        //// GET api/ejemplo
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        var list = EjemploBC.GetAll();
        //        return Request.CreateResponse(HttpStatusCode.OK, list);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, "mensaje");
        //    }
        //}

        //// GET api/ejemplo/5
        //public HttpResponseMessage Get(int id)
        //{
        //    try
        //    {
        //        var entity = EjemploBC.GetById(id);

        //        if (entity != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        else
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}

        //// POST api/ejemplo
        //public HttpResponseMessage Post([FromBody]Ejemplo entity)
        //{
        //    try
        //    {
        //        EjemploBC.Add(entity);

        //        var message = Request.CreateResponse(HttpStatusCode.Created, entity);
        //        message.Headers.Location = new Uri(Request.RequestUri + entity.Id.ToString());
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}

        //// PUT api/ejemplo/5
        //public HttpResponseMessage Put(int id, [FromBody]Ejemplo entity)
        //{
        //    try
        //    {
        //        var entityFound = EjemploBC.GetById(id);

        //        if (entityFound != null)
        //        {
        //            EjemploBC.Update(id, entity);
        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}

        //// DELETE api/ejemplo/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {
        //        var entityFound = EjemploBC.GetById(id);

        //        if (entityFound != null)
        //        {
        //            EjemploBC.Delete(entityFound);
        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}

    }
}
