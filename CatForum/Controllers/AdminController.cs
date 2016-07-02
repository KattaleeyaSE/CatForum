using CatForum.Interfaces;
using CatForum.Models;
using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class AdminController : Controller
    {
        public IAdminRepository admins { get; set; }
        public IReportRepository reports { get; set; }
        public IUserRepository users { get; set; }
        public AdminController()
        {
            this.admins = new AdminRepository();
            this.reports = new ReportRepository();
            this.users = new UserRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Members()
        {
            ViewBag.Members = users.SelectAll();
            return View();
        }
        public ActionResult Reports()
        {
            ViewBag.Reports = reports.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin isValid = admins.Validate(admin);
                if (isValid != null)
                {
                    Session["Admin"] = isValid;
                    return RedirectToAction("Reports", "Admin");
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}
