using System.Web.Mvc;
using System.Web.Routing;

namespace AgateNomad.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Show a 404 error page for anything else.
            routes.MapRoute("Error", "{*url}",
                new { controller = "CatchAll", action = "Index" }
            );
        }
    }
}