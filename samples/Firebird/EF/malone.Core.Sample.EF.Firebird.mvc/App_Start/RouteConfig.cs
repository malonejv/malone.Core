using System.Web.Mvc;
using System.Web.Routing;

namespace malone.Core.Sample.EF.Firebird.mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "List", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditingTasks",
                url: "{controller}/{action}/{listId}/{taskId}",
                defaults: new { controller = "List", action = "EditTask" }
            );
        }
    }
}
