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
        FollowRepository follows { get; set; }
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
            this.follows = new FollowRepository();
        }
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }
        [Route("/Forum/Detail/{id?}")]
        public ActionResult Detail(int id)
        {
            PostDetail post = this.details.SelectById(id);
            ViewBag.Post = post;
            return View();
        }

        [Route("/Forum/Follow/{id?}")]
        public ActionResult Follow(int id)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                if (follows.SearchByUserAndPost(user.Id, id) == null) {
                    Follow follow = new Follow();
                    follow.UserId = user.Id;
                    follow.PostId = id;
                    follows.Add(follow);
                    follows.Save();
                }
            }
                
            return RedirectToAction("Detail", "Forum", new { id = id });
        }

        [Route("/Forum/Unfollow/{id?}")]
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
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.ByProvince = details.SelectByProvince(user.Address.ProvinceId);
            }
            ViewBag.SearchResult = details.Search(Type, Offset, Eyes, Coat, Pattern, Tail, Breed, Province, Amphur, Tumbon);
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
            if (Session["user"] == null)
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
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                if (/*ModelState.IsValid && */Session["User"] != null)
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
                    return RedirectToAction("Forums", "User");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Forums", "User");
        }
        public ActionResult Delete(int Id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            PostDetail post = this.details.SelectById(Id);
            post.Status = 2;
            details.Update(post);
            details.Save();
            return RedirectToAction("Forums", "User");
        }
    }
}