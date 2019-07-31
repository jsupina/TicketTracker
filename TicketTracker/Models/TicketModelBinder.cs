using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketTracker.Models
{
    public class TicketModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string name = request.Form.Get("Name");
            string severity = request.Form.Get("Severity");
            string user = request.Form.Get("User");
            string description = request.Form.Get("Description");

            return new Ticket()
            {
                Id = 0,
                Date = @DateTime.Now,
                Name = name,
                Severity = severity,
                Status = "Open",
                User = user,
                Description = description
            };
        }
    }
}

