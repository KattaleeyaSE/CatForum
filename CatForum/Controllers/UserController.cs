using CatForum.Forms;
using CatForum.Interfaces;
using CatForum.Models;
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
        public IUserRepository repository { get; set; }
        public IAddressRepository addressRepository { get; set; }
        public IPostTypeRepository typeRepository { get; set; }
        public IPostRepository postRepository { get; set; }
        public IPostDetailRepository detailRepository { get; set; }
        public IProvinceRepository prov { get; set; }
        public IAmphurRepository amp { get; set; }
        public ITumbonRepository tmbn { get; set; }
        PictureRepository pictures { get; set; }
        public UserController()
        {
            this.repository = new UserRepository();
            this.addressRepository = new AddressRepository();
            this.typeRepository = new PostTypeRepository();
            this.postRepository = new PostRepository();
            this.detailRepository = new PostDetailRepository();
            this.prov = new ProvinceRepository();
            this.amp = new AmphurRepository();
            this.tmbn = new TumbonRepository();
            this.pictures = new PictureRepository();
        }
        // GET: User
        public ActionResult Edit()
        {
            ViewBag.Provinces = prov.SelectAll();
            ViewBag.Amphurs = amp.SelectAll();
            ViewBag.Tumbons = tmbn.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserEditForm form)
        {
            try
            {
                if (/*ModelState.IsValid && */Session["User"] != null)
                {
                    User temp = (User)Session["User"];
                    User old = repository.SelectById(temp.Id);
                    if (old.Username != form.Username) {
                        var exist = repository.SelectAll().Where(u => u.Username == form.Username && u.Id != old.Id).FirstOrDefault();
                        if (exist != null)
                        {
                            Session["Error"] = "User name is already exist.";
                            return RedirectToAction("Edit", "User");
                        }
                    }
                    old.Username = form.Username;
                    if (form.Password != null && form.RePassword != null && form.Password.Equals(form.RePassword))
                    {
                        old.Password = form.Password;
                    }
                    old.Email = form.Email;
                    old.DisplayName = form.DisplayName;
                    old.Gender = form.Gender;
                    old.Address.ProvinceId = form.Province;
                    old.Address.AmphurId = form.Amphur;
                    old.Address.TumbonId = form.Tumbon;
                    if (form.File != null && form.File.ContentLength > 0)
                    {
                        var picture = new Picture
                        {
                            Filename = System.IO.Path.GetFileName(form.File.FileName),
                            ContentType = form.File.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(form.File.InputStream))
                        {
                            picture.Content = reader.ReadBytes(form.File.ContentLength);
                        }
                        picture.PostId = 0;
                        pictures.Add(picture);
                        pictures.Save();
                        old.ImageId = picture.Id;
                    }
                    repository.Update(old);
                    Session["User"] = old;
                    return RedirectToAction("Edit", "User");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Forums()
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.PostTypes = typeRepository.SelectAll();
                ViewBag.All = postRepository.SelectAll().Where(p => p.UserId == user.Id).OrderByDescending(p => p.Id).ToList();
                ViewBag.Details = detailRepository;
                return View();
            }
            return RedirectToAction("Login", "Home");

        }
    }
}