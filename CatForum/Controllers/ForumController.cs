using CatForum.Forms;
using CatForum.Models;
using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class ForumController : Controller
    {
        ProvinceRepository prov { get; set; }
        AmphurRepository amp { get; set; }
        TumbonRepository tmbn { get; set; }
        CatBreedRepository breeds { get; set; }
        CatCoatRepository coats { get; set; }
        CatEyesRepository eyes { get; set; }
        CatLifeStageRepository lifStages { get; set; }
        CatPatternRepository patterns { get; set; }
        CatTailRepository tails { get; set; }
        PostTypeRepository types { get; set; }
        PostRepository posts { get; set; }
        PostDetailRepository details { get; set; }
        PictureRepository pictures { get; set; }
        UserRepository users { get; set; }
        AddressRepository addresses { get; set; }
        CatRepository cats { get; set; }
        public ForumController()
        {
            this.prov = new ProvinceRepository();
            this.amp = new AmphurRepository();
            this.tmbn = new TumbonRepository();
            this.breeds = new CatBreedRepository();
            this.coats = new CatCoatRepository();
            this.eyes = new CatEyesRepository();
            this.lifStages = new CatLifeStageRepository();
            this.patterns = new CatPatternRepository();
            this.tails = new CatTailRepository();
            this.types = new PostTypeRepository();
            this.posts = new PostRepository();
            this.details = new PostDetailRepository();
            this.pictures = new PictureRepository();
            this.users = new UserRepository();
            this.addresses = new AddressRepository();
            this.cats = new CatRepository();
        }
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.Provinces = prov.SelectAll();
            ViewBag.Amphurs = amp.SelectAll();
            ViewBag.Tumbons = tmbn.SelectAll();

            ViewBag.Breeds = breeds.SelectAll();
            ViewBag.Coats = coats.SelectAll();
            ViewBag.Eyes = eyes.SelectAll();
            ViewBag.LifeStages = lifStages.SelectAll();
            ViewBag.Patterns = patterns.SelectAll();
            ViewBag.Tails = tails.SelectAll();

            ViewBag.Types = types.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult Create(PostForm form) {
            try
            {
                if (ModelState.IsValid && Session["User"] != null)
                {
                    User user = (User)Session["User"];
                    Post post = new Post();
                    post.UserId = user.Id;
                    post.Name = form.PostTitle;
                    post.Content = form.PostDetail;
                    post.Created = DateTime.Now;
                    post.Updated = DateTime.Now;

                    Cat cat = new Cat();
                    cat.Name = form.CatName;
                    cat.Age = form.CatAge;
                    cat.Gender = form.Gender;
                    cat.LifeStageId = form.LifeStage;
                    cat.EyesId = form.CatEyes;
                    cat.CoatId = form.CatCoat;
                    cat.PatternId = form.CatPattern;
                    cat.TailId = form.CatTail;
                    cat.BreedId = form.CatBreed;
                    cat.FoodLike = form.FoodLike;
                    cat.FoodDislike = form.FoodDislike;
                    cat.Habit = form.Habit;
                    cat.Hate = form.Hate;
                    cat.Vaccine = form.Vaccine;
                    cat.Description = form.CatDescription;
                    cat.Status = form.CatStatus;

                    Address address = new Address();
                    address.ProvinceId = form.Province;
                    address.AmphurId = form.Amphur;
                    address.TumbonId = form.Tumbon;

                    posts.Add(post);
                    cats.Add(cat);
                    addresses.Add(address);

                    PostDetail detail = new PostDetail();
                    detail.PostId = post.Id;
                    detail.CatId = cat.Id;
                    detail.AddressId = address.Id;
                    detail.TypeId = form.PostType;
                    detail.Status = form.PostStatus;
                    detail.Condition = form.Condition;
                    detail.Contact = form.Contact;
                    detail.Fee = form.Fee;
                    detail.Location = form.Location;

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
                        picture.PostId = post.Id;
                        pictures.Add(picture);
                    }
                    details.Add(detail);
                    return RedirectToAction("Forums","Home");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }
    }
}