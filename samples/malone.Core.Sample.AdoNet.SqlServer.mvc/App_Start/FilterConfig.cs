using System.Web;
using System.Web.Mvc;
using malone.Core.Sample.AdoNet.SqlServer.mvc.Attributes;

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
