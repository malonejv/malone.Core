using ErgaOmnes.Core.BL;
using ErgaOmnes.Core.EL.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ErgaOmnes.Api.Controllers
{
    //[Authorize]
    public class EjemploController : ApiController
    {
        private IEjemploBC EjemploBC { get; set; }

        public EjemploController(IEjemploBC ejemploBC)
        {
            EjemploBC = ejemploBC;
        }

        // GET api/ejemplo
        public HttpResponseMessage Get()
        {
            try
            {
                var list = EjemploBC.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "mensaje");
            }
        }

        // GET api/ejemplo/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var entity = EjemploBC.GetById(id);

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
        public HttpResponseMessage Post([FromBody]Ejemplo entity)
        {
            try
            {
                EjemploBC.Add(entity);

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
        public HttpResponseMessage Put(int id, [FromBody]Ejemplo entity)
        {
            try
            {
                var entityFound = EjemploBC.GetById(id);

                if (entityFound != null)
                {
                    EjemploBC.Update(id, entity);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entityFound = EjemploBC.GetById(id);

                if (entityFound != null)
                {
                    EjemploBC.Delete(entityFound);
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
}
