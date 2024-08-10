using System.Web.Mvc;
using System.Web.Routing;

namespace TaskManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "{area:exists}/{controller}/{action}/{id}",
            //    defaults: new { area="Admin", controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}