using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using TalkToLog.Bot.Filters;

namespace TalkToLog.Bot
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("images/mvcproducts/{Metric}/{Number}/default.png",
                new CustomPNGRouteHandler()));

        }
    }
}