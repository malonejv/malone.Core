using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Log;

namespace malone.Core.CL.Exceptions.Handler.Implementations
{
    public class ExceptionHandler : IExceptionHandler
    {
        protected ILogger Logger { get; }

        public ExceptionHandler(ILogger logger)
        {
            Logger = logger;
        }

        public void HandleException(Exception ex)
        {
            var isBaseException = typeof(BaseException).IsAssignableFrom(ex.GetType());
            if (isBaseException)
            {
                //var isViewException = typeof(ViewException).IsAssignableFrom(ex.GetType());
                if (ex is ViewException)
                {

                }

                //var isBusinessException = typeof(BusinessException).IsAssignableFrom(ex.GetType());
                if (ex is BusinessException)
                {

                }

                //var isBusinessValidationException = typeof(BusinessValidationException).IsAssignableFrom(ex.GetType());
                if (ex is BusinessValidationException)
                {

                }

                //var isDataAccessException = typeof(DataAccessException).IsAssignableFrom(ex.GetType());
                if (ex is DataAccessException)
                {

                }

                //var isServiceAgentException = typeof(ServiceAgentException).IsAssignableFrom(ex.GetType());
                if (ex is ServiceAgentException)
                {

                }
                
                if (Logger != null && (ex as BaseException).ShouldLog)
                    Logger.Error(ex);
                if ((ex as BaseException).Rethrow)
                    throw ex;
            }
            else
            {
                if (Logger != null)
                    Logger.Error(ex);
            }



            //if (log.IsDebugEnabled)
            //{
            //    var loggingWatch = Stopwatch.StartNew();
            //    filterContext.HttpContext.Items.Add(StopwatchKey, loggingWatch);

            //    var message = new StringBuilder();
            //    var controllerName = filterContext.RouteData.Values["controller"];
            //    var actionName = filterContext.RouteData.Values["action"];
            //    message.Append(string.Format("Executing controller {0}, action {1}", controllerName, actionName));
            //    log.Info(message);
            //    log.Error(ex.Message);
            //}


        }
    }
}
