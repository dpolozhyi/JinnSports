using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JinnSports.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
<<<<<<< HEAD:JinnSports.WEB/App_Start/RouteConfig.cs
                defaults: new { controller = "Event", action = "History", id = UrlParameter.Optional });
=======
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
>>>>>>> origin/Two_solutions:JinnSports.MVC/App_Start/RouteConfig.cs
        }
    }
}
