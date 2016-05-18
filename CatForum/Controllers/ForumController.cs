using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class ForumController : Controller
    {
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
            ProvinceRepository prov = new ProvinceRepository();
            ViewBag.Provinces = prov.SelectAll();
            AmphurRepository amp = new AmphurRepository();
            ViewBag.Amphurs = amp.SelectAll();
            TumbonRepository tmbn = new TumbonRepository();
            ViewBag.Tumbons = tmbn.SelectAll();

            CatBreedRepository breeds = new CatBreedRepository();
            ViewBag.Breeds = breeds.SelectAll();
            CatCoatRepository coats = new CatCoatRepository();
            ViewBag.Coats = coats.SelectAll();
            CatEyesRepository eyes = new CatEyesRepository();
            ViewBag.Eyes = eyes.SelectAll();
            CatLifeStageRepository lifStages = new CatLifeStageRepository();
            ViewBag.LifeStages = lifStages.SelectAll();
            CatPatternRepository patterns = new CatPatternRepository();
            ViewBag.Pattern = patterns.SelectAll();
            CatTailRepository tails = new CatTailRepository();
            ViewBag.Tail = tails.SelectAll();

            PostTypeRepository types = new PostTypeRepository();
            ViewBag.Types = types.SelectAll();
            return View();
        }
    }
}