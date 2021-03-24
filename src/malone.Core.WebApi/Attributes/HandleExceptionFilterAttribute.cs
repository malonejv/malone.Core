using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Localization;
using System;
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
        private IContentLocalizationHandler ContentLocalizationHandler { get; set; }
        private IErrorLocalizationHandler ErrorLocalizationHandler { get; set; }

        public HandleExceptionFilterAttribute()
        {

            ContentLocalizationHandler = ServiceLocator.Current.Get<IContentLocalizationHandler>();
            ErrorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            var ex = actionExecutedContext.Exception;
            var exceptionType = actionExecutedContext.Exception.GetType();

            //Is BaseException
            if (exceptionType == typeof(BusinessRulesValidationException))
            {
                var bvEx = ex as BusinessRulesValidationException;
                StringBuilder sbMessage = new StringBuilder();

                foreach (var val in bvEx.Results)
                {
                    sbMessage.AppendLine(val.Message);
                }

                message = sbMessage.ToString();
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(EntityNotFoundException))
            {
                var notFoundEx = ex as EntityNotFoundException;

                message = notFoundEx.Message;
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(BusinessValidationException))
            {
                var bvEx = ex as BusinessValidationException;

                message = bvEx.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (typeof(BaseException).IsAssignableFrom(exceptionType))
            {
                var baseEx = ex as BaseException;

                string supportText = ContentLocalizationHandler.GetString(CoreContents.Logging_SupportId);
                var supporId = baseEx?.Data[BaseException.SUPPORT_ID]?.ToString();

                message = ErrorLocalizationHandler.GetString(CoreErrors.TECH200, supportText, supporId);
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ErrorLocalizationHandler.GetString(CoreErrors.SERVICE300);
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                message = ErrorLocalizationHandler.GetString(CoreErrors.TECH201);
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
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            var ex = actionExecutedContext.Exception;
            var exceptionType = actionExecutedContext.Exception.GetType();

            //Is BaseException
            if (typeof(BaseException).IsAssignableFrom(exceptionType))
            {
                var baseEx = ex as BaseException;

                string supportText = ContentLocalizationHandler.GetString(CoreContents.Logging_SupportId);
                var supporId = baseEx?.Data[BaseException.SUPPORT_ID]?.ToString();

                message = ErrorLocalizationHandler.GetString(CoreErrors.TECH200, supportText, supporId);
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(BusinessRulesValidationException))
            {
                var bvEx = ex as BusinessRulesValidationException;
                StringBuilder sbMessage = new StringBuilder();

                foreach (var val in bvEx.Results)
                {
                    sbMessage.AppendLine(val.Message);
                }

                message = sbMessage.ToString();
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ErrorLocalizationHandler.GetString(CoreErrors.SERVICE300);
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                message = ErrorLocalizationHandler.GetString(CoreErrors.TECH201);
                status = HttpStatusCode.BadRequest;
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
