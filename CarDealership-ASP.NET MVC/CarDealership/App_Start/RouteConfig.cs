using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealership
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Deetails2",
                url: "Car/{year}/{make}/{Model}",
                defaults: new { controller = "car", action = "details2" }

                );

            routes.MapRoute(
    name: "Deetails3",
    url: "Car/details",
    defaults: new { controller = "car", action = "Index" }

    );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );






        }
    }
}
