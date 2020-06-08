using malone.Core.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace malone.Core.WebApi.Attributes
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //var isFaultException = typeof(BaseException).IsAssignableFrom(actionExecutedContext.Exception.GetType());

            //if (context.Exception is BaseException)
            //{
            //    context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            //}
            base.OnException(actionExecutedContext);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}
