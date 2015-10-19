using System.Web.Mvc;
using System.Web.Routing;

namespace RosterCheck_ASPNET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "GuildIndex",
                url: "g/{realm}/{name}",
                defaults: new { controller = "Guild", action = "Index", realm = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}
