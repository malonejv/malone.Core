using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Exceptions.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace malone.Core.WebApi.Attributes
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ICoreMessageHandler CoreMessageHandler { get; set; }

        public HandleExceptionFilterAttribute()
        {

            CoreMessageHandler = ServiceLocator.Current.Get<ICoreMessageHandler>();
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //var isFaultException = typeof(BaseException).IsAssignableFrom(actionExecutedContext.Exception.GetType());

            //if (context.Exception is BaseException)
            //{
            //    context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            //}
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();

            //Is BaseException
            if (typeof(BaseException).IsAssignableFrom(actionExecutedContext.Exception.GetType()))
            {
                //TODO: DEFINIR UN MÉTODO PARA CONFIGURAR QUE ERRORES SE PUEDEN MOSTRAR AL USUARIO Y CUALES NO
                message = CoreMessageHandler.GetMessage(CoreErrors.TECH1);
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = CoreMessageHandler.GetMessage(CoreErrors.SERVICE300);
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                message = CoreMessageHandler.GetMessage(CoreErrors.TECH2);
                status = HttpStatusCode.BadRequest;
            }

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };

            base.OnException(actionExecutedContext);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            HttpStatusCode status = HttpStatusCode.BadRequest;
            String message = String.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();

            //Is BaseException
            if (typeof(BaseException).IsAssignableFrom(actionExecutedContext.Exception.GetType()))
            {
                //TODO: DEFINIR UN MÉTODO PARA CONFIGURAR QUE ERRORES SE PUEDEN MOSTRAR AL USUARIO Y CUALES NO
                message = CoreMessageHandler.GetMessage(CoreErrors.TECH1);
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = CoreMessageHandler.GetMessage(CoreErrors.SERVICE300);
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                message = CoreMessageHandler.GetMessage(CoreErrors.TECH2);
                status = HttpStatusCode.NotFound;
            }

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };

            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}
