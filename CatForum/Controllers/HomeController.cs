using CatForum.Forms;
using CatForum.Interfaces;
using CatForum.Models;
using CatForum.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
        public IUserRepository repository { get; set; }
        public IAddressRepository addressRepository { get; set; }
        public IPostDetailRepository detailRepository { get; set; }
        public IPostAdoptRepository adopts { get; set; }
        public IProvinceRepository provinces { get; set; }
        public IAmphurRepository amphurs { get; set; }
        public ITumbonRepository tumbons { get; set; }
        public HomeController()
        {
            this.repository = new UserRepository();
            this.addressRepository = new AddressRepository();
            this.detailRepository = new PostDetailRepository();
            this.adopts = new PostAdoptRepository();
            this.provinces = new ProvinceRepository();
            this.amphurs = new AmphurRepository();
            this.tumbons = new TumbonRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.PostRepo = detailRepository;
            ViewBag.Adopts = null;
            ViewBag.Match = null;
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.Adopts = adopts.SearchByUser(user.Id).Take(10).ToList();
                ViewBag.Matchs = detailRepository.SearchMatch(user.Id).Take(10).ToList();
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User isValid = repository.Validate(user);
                if (isValid != null)
                {
                    Session["User"] = isValid;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Provinces = provinces.SelectAll();
            ViewBag.Amphurs = amphurs.SelectAll();
            ViewBag.Tumbons = tumbons.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterForm form)
        {
            if (ModelState.IsValid)
            {
                if (form.Password.Equals(form.RePassword) && form.Email.Equals(form.ReEmail))
                {
                    Address address = addressRepository.IsAddressExist(form.Province, form.Amphur, form.Tumbon);
                    if (address == null)
                    {
                        address = new Address();
                        address.ProvinceId = form.Province;
                        address.AmphurId = form.Amphur;
                        address.TumbonId = form.Tumbon;
                        addressRepository.Add(address);
                        addressRepository.Save();
                    }

                    User user = new Models.User();
                    user.Username = form.Username;
                    user.Password = form.Password;
                    user.AddressId = address.Id;
                    var isValid = repository.Register(user);
                    if (isValid != 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        public JsonResult Provinces()
        {
            var json = JsonConvert.SerializeObject(provinces.SelectAll().ToList(), Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Amphurs(int id)
        {
            var json = JsonConvert.SerializeObject(amphurs.SelectByProvince(id).ToList(), Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Tumbons(int id)
        {
            var json = JsonConvert.SerializeObject(tumbons.SelectByAmphur(id).ToList(), Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}