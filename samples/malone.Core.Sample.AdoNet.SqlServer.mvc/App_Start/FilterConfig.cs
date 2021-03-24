using malone.Core.Sample.AdoNet.SqlServer.mvc.Attributes;
using System.Web.Mvc;

namespace malone.Core.Sample.AdoNet.SqlServer.mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandlerAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
