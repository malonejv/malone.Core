using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using ErgaOmnes.Core.BL;
using ErgaOmnes.Core.EL.Model;
using ErgaOmnes.Core.EL.RequestParams;
using ErgaOmnes.Core.EL.ViewModel;
using malone.Core.EF.Entities.Filters;
using malone.Core.WebApi;
using Microsoft.Web.Http;

namespace ErgaOmnes.Api.Controllers.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    [RoutePrefix("v{version:apiVersion}/Ejemplo")]
    public class EjemploController : CoreApiController<EjemploGetRequestParam, Ejemplo, IEjemploBC, IEjemploBV>
    {

        public EjemploController(IEjemploBC businessComponent, Mapper mapperInstance) : base(businessComponent, mapperInstance)
        {
        }

        //GET entity
        [HttpPost()]
        //[Route("~/Ejemplo/FilterBy")]
        [Route("FilterBy")]
        public IHttpActionResult FilterBy(EjemploGetRequestParam parameters)
        {
            IEnumerable list = GetList(parameters);

            return Ok(list);
        }



        protected override IEnumerable GetFiltered(EjemploGetRequestParam parameters)
        {
            var ejemploParams = parameters as EjemploGetRequestParam;
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
    }
}
