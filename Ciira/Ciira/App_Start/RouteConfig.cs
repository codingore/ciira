using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ciira
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Gate", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "login.yo",
                defaults: new { controller = "Gate", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout.yo",
                defaults: new { controller = "Gate", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Setup",
                url: "setup.yo",
                defaults: new { controller = "Setup", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Gate", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}