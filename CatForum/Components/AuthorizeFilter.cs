using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatsInTheBox.Components
{
    public class AuthorizeFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Auth"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary
                 {
                    { "controller", "Home" },
                    { "action", "Login" }
                 });
            }
        }
    }
}