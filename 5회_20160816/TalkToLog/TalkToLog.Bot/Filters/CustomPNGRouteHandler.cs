using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TalkToLog.Bot.Filters
{
    public class CustomPNGRouteHandler : IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CustomPNGHandler(requestContext);
        }
    }

}