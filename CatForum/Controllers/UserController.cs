using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Edit()
        {
            return View();
        }

    }
}