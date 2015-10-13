using System.Web.Mvc;
using System.Web.Routing;

namespace PL.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "User",
                url: "{id}",
                defaults: new {controller = "Home", action = "Profile"});

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
           
        }
    }
}