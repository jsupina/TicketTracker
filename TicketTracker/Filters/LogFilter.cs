using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Serilog;

namespace TicketTracker.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public LogFilter()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .CreateLogger();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogRequest("OnActionExecuting", filterContext.RouteData);
        }

        private void LogRequest(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            Log.Information("Action: {actionName} \n Controller: {controllerName} \n Time: {now}", actionName, controllerName, DateTime.Now);
        }

    }
}