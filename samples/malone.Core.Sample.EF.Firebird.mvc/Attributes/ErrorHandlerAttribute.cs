using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Localization;
using malone.Core.Commons.Log;

namespace malone.Core.Sample.EF.Firebird.mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        //private IContentLocalizationHandler ContentLocalizationHandler { get; set; }
        //private IErrorLocalizationHandler ErrorLocalizationHandler { get; set; }

        public ILogger Logger { get; set; }

        public ErrorHandlerAttribute()
        {
            Logger = ServiceLocator.Current.Get<ILogger>(); ;
            //ContentLocalizationHandler = ServiceLocator.Current.Get<IContentLocalizationHandler>();
            //ErrorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
        }

        public void OnException(ExceptionContext filterContext)
        {
            HttpCookie cookie = filterContext.HttpContext.Request.Cookies["_culture"];
            string culture = "";
            if (cookie != null)
                culture = cookie.Value;
            //else
            //    culture = CultureHelper.GetImplementedCulture(null);
            CultureInfo ci = new CultureInfo(culture);

            StringBuilder sbMessage = new StringBuilder();
            String message = "Se produjo un error inesperado";
            var ex = filterContext.Exception;
            var exceptionType = filterContext.Exception.GetType();

            var error = new ErrorMessage
            {
                Header = "Error",
                Paragraphs = new List<string>(),
                Status = "danger"
            };

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //Is BaseException
                if (exceptionType == typeof(BusinessRulesValidationException))
                {
                    var bvEx = ex as BusinessRulesValidationException;

                    foreach (var val in bvEx.Results)
                    {
                        error.Paragraphs.Add(val.Message);
                    }
                }
                else
                {
                    error.Paragraphs.Add(message);
                }

                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult
                {
                    Data = error,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //Is BaseException
                if (exceptionType == typeof(BusinessRulesValidationException))
                {
                    var bvEx = ex as BusinessRulesValidationException;

                    foreach (var val in bvEx.Results)
                    {
                        ((Controller)filterContext.Controller).ModelState.AddModelError(val.ErrorCode, val.Message);
                        error.Paragraphs.Add(val.Message);
                    }
                }
                else
                {
                    error.Paragraphs.Add(message);
                }

                //if (filterContext.Controller.TempData != null && !filterContext.Controller.TempData.ContainsKey("ErrorMessage"))
                //    filterContext.Controller.TempData["ErrorMessage"] = error;
                filterContext.HttpContext.Session["ErrorMessage"] = error;

                var rawUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;

                string url = "/";
                if (!string.IsNullOrEmpty(rawUrl))
                {
                    url = rawUrl;

                    if (rawUrl.IndexOf("/", 1) != -1)
                        url = rawUrl.Substring(0, rawUrl.IndexOf("/", 1));
                }
                filterContext.HttpContext.Response.Redirect(url);
            }

            Logger.Error(ex);

            filterContext.ExceptionHandled = true;
        }

    }

    public class ErrorMessage
    {
        public string Header{ get; set; }
        public List<string> Paragraphs { get; set; }
        public string Status { get; set; }
    }

}