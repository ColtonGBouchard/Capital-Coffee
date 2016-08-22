using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CapitalCoffee
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

            routes.MapRoute(
                name: "ShopDetails",
                url: "{controller}/{action}/{shopId}",
                defaults: new { controller = "Shop", action = "Details", shopId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddReview",
                url: "{controller}/{action}/{shopId}",
                defaults: new { controller = "Review", action = "Add", shopId = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ReviewEdit",
                url: "{controller}/{action}/{reviewId}",
                defaults: new { controller = "Review", action = "Edit", reviewId = UrlParameter.Optional }
            );

            
            
        }
    }
}
