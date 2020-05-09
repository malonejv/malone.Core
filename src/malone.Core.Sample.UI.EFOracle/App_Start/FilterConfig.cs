using System.Web;
using System.Web.Mvc;

namespace malone.Core.Sample.UI.EFOracle
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
