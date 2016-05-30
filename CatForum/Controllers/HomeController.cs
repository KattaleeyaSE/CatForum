using CatForum.Forms;
using CatForum.Models;
using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
        public UserRepository repository { get; set; }
        public AddressRepository addressRepository { get; set; }
        public PostDetailRepository detailRepository { get; set; }
        public PostAdoptRepository adopts { get; set; }
        public HomeController()
        {
            this.repository = new UserRepository();
            this.addressRepository = new AddressRepository();
            this.detailRepository = new PostDetailRepository();
            this.adopts = new PostAdoptRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.PostRepo = detailRepository;
            ViewBag.Adopts = null;
            if (Session["User"] != null) {
                User user = (User)Session["User"];
                ViewBag.Adopts = adopts.SearchByUser(user.Id);
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
            ProvinceRepository prov = new ProvinceRepository();
            ViewBag.Provinces = prov.SelectAll();
            AmphurRepository amp = new AmphurRepository();
            ViewBag.Amphurs = amp.SelectAll();
            TumbonRepository tmbn = new TumbonRepository();
            ViewBag.Tumbons = tmbn.SelectAll();
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
    }
}