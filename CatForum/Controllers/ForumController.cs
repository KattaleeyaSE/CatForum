using CatForum.Forms;
using CatForum.Interfaces;
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
        public IProvinceRepository prov { get; set; }
        public IAmphurRepository amp { get; set; }
        public ITumbonRepository tmbn { get; set; }
        public ICatBreedRepository breeds { get; set; }
        public ICatCoatRepository coats { get; set; }
        public ICatEyesRepository eyes { get; set; }
        public ICatLifeStageRepository lifStages { get; set; }
        public ICatPatternRepository patterns { get; set; }
        public ICatTailRepository tails { get; set; }
        public IPostTypeRepository types { get; set; }
        public IPostRepository posts { get; set; }
        public IPostDetailRepository details { get; set; }
        public IPostAdoptRepository adopts { get; set; }
        public IPictureRepository pictures { get; set; }
        public IUserRepository users { get; set; }
        public IAddressRepository addresses { get; set; }
        public ICatRepository cats { get; set; }
        public IFollowRepository follows { get; set; }
        public IReportRepository reports { get; set; }
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
            this.adopts = new PostAdoptRepository();
            this.pictures = new PictureRepository();
            this.users = new UserRepository();
            this.addresses = new AddressRepository();
            this.cats = new CatRepository();
            this.follows = new FollowRepository();
            this.reports = new ReportRepository();
        }
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }
        [Route("Detail/{id?}")]
        public ActionResult Detail(int id)
        {
            PostDetail post = this.details.SelectById(id);
            ViewBag.Post = post;
            return View();
        }

        public ActionResult Follow(int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                if (follows.SearchByUserAndPost(user.Id, id) == null)
                {
                    Follow follow = new Follow();
                    follow.UserId = user.Id;
                    follow.PostId = id;
                    follows.Add(follow);
                    follows.Save();
                }
            }
            return RedirectToAction("Detail", "Forum", new { id = id });
        }
        public ActionResult UnFollow(int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                Follow isExist = follows.SearchByUserAndPost(user.Id, id);
                if (isExist != null)
                {
                    follows.Delete(isExist.Id);
                    follows.Save();
                }
            }

            return RedirectToAction("Detail", "Forum", new { id = id });
        }
        [HttpPost]
        public ActionResult Adopt(PostAdopt adopt)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                PostAdopt isExist = adopts.IsExist(user.Id, adopt.Id);
                if (isExist == null)
                {
                    isExist = new PostAdopt();
                    isExist.UserId = user.Id;
                    isExist.PostId = adopt.Id;
                    isExist.Detail = adopt.Detail;
                    isExist.Contact = adopt.Contact;
                    isExist.Status = 1;
                    adopts.Add(isExist);
                    adopts.Save();
                }
            }
            return RedirectToAction("Detail", "Forum", new { id = adopt.Id });
        }
        [Route("/Forum/Accept/{id?}")]
        public ActionResult Accept(int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                PostAdopt isExist = adopts.SelectById(id);
                if (isExist != null)
                {
                    isExist.Status = 2;
                    adopts.Update(isExist);
                }
            }

            //return RedirectToAction("Detail", "Forum", new { id = id });
            return RedirectToAction("Index", "Home");
        }
        [Route("/Forum/Reject/{id?}")]
        public ActionResult Reject(int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                PostAdopt isExist = adopts.SelectById(id);
                if (isExist != null)
                {
                    isExist.Status = 3;
                    adopts.Update(isExist);
                }
            }

            //return RedirectToAction("Detail", "Forum", new { id = id });
            return RedirectToAction("Index", "Home");
        }
        [Route("/Forum/Report/{id?}"), HttpPost]
        public ActionResult Report(Report form, int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                Report report = new Report();
                report.Created = DateTime.Now;
                report.PostId = id;
                report.UserId = user.Id;
                report.Text = form.Text;
                reports.Add(report);
                reports.Save();
            }
            return RedirectToAction("Detail", "Forum", new { id = id });
        }
        public ActionResult Search(Nullable<int> Type, Nullable<int> Offset, Nullable<int> Eyes, Nullable<int> Coat, Nullable<int> Pattern, Nullable<int> Tail, Nullable<int> Breed, Nullable<int> Province, Nullable<int> Amphur, Nullable<int> Tumbon)
        {
            /*Address*/
            ViewBag.Provinces = prov.SelectAll();
            ViewBag.Amphurs = amp.SelectAll();
            ViewBag.Tumbons = tmbn.SelectAll();

            /*Cat*/
            ViewBag.Breeds = breeds.SelectAll();
            ViewBag.Coats = coats.SelectAll();
            ViewBag.Eyes = eyes.SelectAll();
            ViewBag.LifeStages = lifStages.SelectAll();
            ViewBag.Patterns = patterns.SelectAll();
            ViewBag.Tails = tails.SelectAll();

            /*Query String*/
            ViewBag.Province = Province;
            ViewBag.Amphur = Amphur;
            ViewBag.Tumbon = Tumbon;
            ViewBag.Breed = Breed;
            ViewBag.Coat = Coat;
            ViewBag.Eye = Eyes;
            ViewBag.Pattern = Pattern;
            ViewBag.Tail = Tail;
            ViewBag.Type = Type;
            ViewBag.Offset = Offset;

            /*Result*/
            ViewBag.Types = types.SelectAll();
            ViewBag.All = details.SelectDescDate(Type, Offset);
            ViewBag.ByProvince = null;
            ViewBag.SearchResult = null;

            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.ByProvince = details.SelectByProvince(user.Address.ProvinceId);
            }
            if (Eyes != null || Coat != null || Pattern != null || Tail != null || Breed != null || Province != null || Amphur != null || Tumbon != null)
            {
                ViewBag.SearchResult = details.Search(Type, Offset, Eyes, Coat, Pattern, Tail, Breed, Province, Amphur, Tumbon);
            }
            return View();
        }
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
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
        public ActionResult Create(PostForm form)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                if (/*ModelState.IsValid && */Session["User"] != null)
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
                    posts.Save();
                    cats.Add(cat);
                    cats.Save();
                    addresses.Add(address);
                    addresses.Save();

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
                        pictures.Save();
                    }
                    details.Add(detail);
                    details.Save();
                    return RedirectToAction("Forums", "User");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Create", "Forum");
        }
        public ActionResult Edit(int Id)
        {
            if (Session["User"] != null && Session["Admin"] != null)
            {
                return RedirectToAction("Login", "Home");
            }
            PostDetail post = this.details.SelectById(Id);
            ViewBag.Post = post;

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
        public ActionResult Edit(PostForm form, int Id)
        {
            if (Session["User"] != null && Session["Admin"] != null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                if (/*ModelState.IsValid && */Session["User"] != null || Session["Admin"] != null)
                {
                    PostDetail detail = details.SelectById(Id);
                    Post post = detail.Post;
                    post.Name = form.PostTitle;
                    post.Content = form.PostDetail;
                    post.Updated = DateTime.Now;

                    Cat cat = detail.Cat;
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

                    Address address = detail.Address;
                    address.ProvinceId = form.Province;
                    address.AmphurId = form.Amphur;
                    address.TumbonId = form.Tumbon;

                    posts.Update(post);
                    posts.Save();
                    cats.Update(cat);
                    cats.Save();
                    addresses.Update(address);
                    addresses.Save();

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
                        pictures.Save();
                    }
                    details.Update(detail);
                    details.Save();
                    if (Session["Admin"] != null) {
                        return RedirectToAction("Reports", "Admin");
                    }
                    return RedirectToAction("Forums", "User");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Reports", "Admin");
            }
            return RedirectToAction("Forums", "User");
        }
        public ActionResult Delete(int Id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            PostDetail post = this.details.SelectById(Id);
            post.Status = 2;
            details.Update(post);
            details.Delete(post.Id);
            details.Save();
            return RedirectToAction("Forums", "User");
        }
        public ActionResult Disable(int Id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            PostDetail post = this.details.SelectById(Id);
            post.Status = 2;
            details.Update(post);
            details.Save();
            return RedirectToAction("Reports", "Admin");
        }
    }
}