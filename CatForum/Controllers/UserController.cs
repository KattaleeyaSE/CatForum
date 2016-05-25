using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class UserController : Controller
    {
        public UserRepository repository { get; set; }
        public AddressRepository addressRepository { get; set; }
        public PostTypeRepository typeRepository { get; set; }
        public UserController()
        {
            this.repository = new UserRepository();
            this.addressRepository = new AddressRepository();
            this.typeRepository = new PostTypeRepository();
        }
        // GET: User
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Forums()
        {
            ViewBag.PostTypes = typeRepository.SelectAll();
            return View();
        }
    }
}