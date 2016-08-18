using CatForum.Controllers;
using CatForum.Forms;
using CatForum.Interfaces;
using CatForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatTest.Controllers
{
    [TestClass]
    public class ForumControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var controller = new ForumController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void DetailTest()
        {
            var repo = new Mock<IPostDetailRepository>();
            repo.Setup(x => x.SelectById(1)).Returns(new PostDetail());

            var controller = new ForumController();
            controller.details = repo.Object;

            var result = controller.Detail(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData["Post"], typeof(PostDetail));
        }
        [TestMethod]
        public void FollowTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IFollowRepository>();
            //repo.Setup(x => x.Add(new Follow())).Returns<Follow>(null);
            repo.Setup(x => x.SearchByUserAndPost(1, 1)).Returns<Follow>(null);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SelectById(1)).Returns(new User());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.follows = repo.Object;
            controller.users = userRepo.Object;

            var result = controller.Follow(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(1), "Detail");

        }
        [TestMethod]
        public void UnfollowTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IFollowRepository>();
            //repo.Setup(x => x.Add(new Follow())).Returns<Follow>(null);
            repo.Setup(x => x.SearchByUserAndPost(1, 1)).Returns(new Follow());

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SelectById(1)).Returns(new User());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.follows = repo.Object;
            controller.users = userRepo.Object;

            var result = controller.UnFollow(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(1), "Detail");

        }
        [TestMethod]
        public void AdoptTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IPostAdoptRepository>();
            //repo.Setup(x => x.Add(new Follow())).Returns<Follow>(null);
            repo.Setup(x => x.IsExist(1, 1)).Returns<PostAdopt>(null);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SelectById(1)).Returns(new User());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.adopts = repo.Object;
            controller.users = userRepo.Object;


            var adopt = new PostAdopt();
            adopt.Id = 1;

            var result = controller.Adopt(adopt) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(1), "Detail");

        }
        [TestMethod]
        public void AcceptTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var adopt = new PostAdopt();
            var post = new PostDetail();
            post.Cat = new Cat();
            adopt.Id = 1;
            adopt.Post = post;

            var repo = new Mock<IPostAdoptRepository>();
            //repo.Setup(x => x.Add(new Follow())).Returns<Follow>(null);
            repo.Setup(x => x.SelectById(1)).Returns(adopt);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SelectById(1)).Returns(new User());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.adopts = repo.Object;
            controller.users = userRepo.Object;

            var result = controller.Accept(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");

        }
        [TestMethod]
        public void RejectTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IPostAdoptRepository>();
            //repo.Setup(x => x.Add(new Follow())).Returns<Follow>(null);
            repo.Setup(x => x.SelectById(1)).Returns(new PostAdopt());

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SelectById(1)).Returns(new User());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.adopts = repo.Object;
            controller.users = userRepo.Object;

            var adopt = new PostAdopt();
            adopt.Id = 1;

            var result = controller.Reject(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");

        }
        [TestMethod]
        public void ReportTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IReportRepository>();

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.reports = repo.Object;

            var adopt = new PostAdopt();
            adopt.Id = 1;

            var result = controller.Report(new Report(), 1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 1);
            Assert.AreEqual(result.RouteValues.Values.ElementAt(1), "Detail");

        }
        [TestMethod]
        public void SearchTest()
        {
            var user = new User();
            user.Id = 1;
            user.Address = new Address();
            user.Address.ProvinceId = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var provinces = new Mock<IProvinceRepository>();
            provinces.Setup(x => x.SelectAll()).Returns<List<Province>>(null);
            var amphur = new Mock<IAmphurRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Amphur>>(null);
            var tumbon = new Mock<ITumbonRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Tumbon>>(null);

            var breeds = new Mock<ICatBreedRepository>();
            breeds.Setup(x => x.SelectAll()).Returns<List<CatBreed>>(null);
            var coats = new Mock<ICatCoatRepository>();
            coats.Setup(x => x.SelectAll()).Returns<List<CatCoat>>(null);
            var eyes = new Mock<ICatEyesRepository>();
            eyes.Setup(x => x.SelectAll()).Returns<List<CatEyes>>(null);
            var lifestage = new Mock<ICatLifeStageRepository>();
            lifestage.Setup(x => x.SelectAll()).Returns<List<CatLifeStage>>(null);
            var patterns = new Mock<ICatPatternRepository>();
            patterns.Setup(x => x.SelectAll()).Returns<List<CatPattern>>(null);
            var tails = new Mock<ICatTailRepository>();
            tails.Setup(x => x.SelectAll()).Returns<List<CatTail>>(null);

            var types = new Mock<IPostTypeRepository>();
            types.Setup(x => x.SelectAll()).Returns<List<PostType>>(null);

            var details = new Mock<IPostDetailRepository>();
            details.Setup(x => x.SelectAll()).Returns<List<PostDetail>>(null);
            details.Setup(x => x.SelectDescDate(0, 0)).Returns<List<PostDetail>>(null);
            details.Setup(x => x.SelectByProvince(0)).Returns<List<PostDetail>>(null);
            details.Setup(x => x.Search(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)).Returns<List<PostDetail>>(null);


            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.prov = provinces.Object;
            controller.amp = amphur.Object;
            controller.tmbn = tumbon.Object;
            controller.breeds = breeds.Object;
            controller.coats = coats.Object;
            controller.eyes = eyes.Object;
            controller.lifStages = lifestage.Object;
            controller.patterns = patterns.Object;
            controller.tails = tails.Object;
            controller.types = types.Object;
            controller.details = details.Object;

            var result = controller.Search(0, 0, 0, 0, 0, 0, 0, 0, 0, 0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateGetTest()
        {
            var user = new User();
            user.Id = 1;
            user.Address = new Address();
            user.Address.ProvinceId = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var provinces = new Mock<IProvinceRepository>();
            provinces.Setup(x => x.SelectAll()).Returns<List<Province>>(null);
            var amphur = new Mock<IAmphurRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Amphur>>(null);
            var tumbon = new Mock<ITumbonRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Tumbon>>(null);

            var breeds = new Mock<ICatBreedRepository>();
            breeds.Setup(x => x.SelectAll()).Returns<List<CatBreed>>(null);
            var coats = new Mock<ICatCoatRepository>();
            coats.Setup(x => x.SelectAll()).Returns<List<CatCoat>>(null);
            var eyes = new Mock<ICatEyesRepository>();
            eyes.Setup(x => x.SelectAll()).Returns<List<CatEyes>>(null);
            var lifestage = new Mock<ICatLifeStageRepository>();
            lifestage.Setup(x => x.SelectAll()).Returns<List<CatLifeStage>>(null);
            var patterns = new Mock<ICatPatternRepository>();
            patterns.Setup(x => x.SelectAll()).Returns<List<CatPattern>>(null);
            var tails = new Mock<ICatTailRepository>();
            tails.Setup(x => x.SelectAll()).Returns<List<CatTail>>(null);

            var types = new Mock<IPostTypeRepository>();
            types.Setup(x => x.SelectAll()).Returns<List<PostType>>(null);

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.prov = provinces.Object;
            controller.amp = amphur.Object;
            controller.tmbn = tumbon.Object;
            controller.breeds = breeds.Object;
            controller.coats = coats.Object;
            controller.eyes = eyes.Object;
            controller.lifStages = lifestage.Object;
            controller.patterns = patterns.Object;
            controller.tails = tails.Object;
            controller.types = types.Object;

            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void CreatePostTest()
        {
            var user = new User();
            user.Id = 1;

            var post = new PostForm();

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var posts = new Mock<IPostRepository>();
            var cats = new Mock<ICatRepository>();
            var addresses = new Mock<IAddressRepository>();
            var details = new Mock<IPostDetailRepository>();
            var picture = new Mock<IPictureRepository>();

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.posts = posts.Object;
            controller.cats = cats.Object;
            controller.addresses = addresses.Object;
            controller.details = details.Object;
            controller.pictures = picture.Object;

            var result = controller.Create(post) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Forums");
        }
        [TestMethod]
        public void EditGetTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var details = new Mock<IPostDetailRepository>();
            details.Setup(x => x.SelectById(1)).Returns(new PostDetail());

            var provinces = new Mock<IProvinceRepository>();
            provinces.Setup(x => x.SelectAll()).Returns<List<Province>>(null);
            var amphur = new Mock<IAmphurRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Amphur>>(null);
            var tumbon = new Mock<ITumbonRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Tumbon>>(null);

            var breeds = new Mock<ICatBreedRepository>();
            breeds.Setup(x => x.SelectAll()).Returns<List<CatBreed>>(null);
            var coats = new Mock<ICatCoatRepository>();
            coats.Setup(x => x.SelectAll()).Returns<List<CatCoat>>(null);
            var eyes = new Mock<ICatEyesRepository>();
            eyes.Setup(x => x.SelectAll()).Returns<List<CatEyes>>(null);
            var lifestage = new Mock<ICatLifeStageRepository>();
            lifestage.Setup(x => x.SelectAll()).Returns<List<CatLifeStage>>(null);
            var patterns = new Mock<ICatPatternRepository>();
            patterns.Setup(x => x.SelectAll()).Returns<List<CatPattern>>(null);
            var tails = new Mock<ICatTailRepository>();
            tails.Setup(x => x.SelectAll()).Returns<List<CatTail>>(null);

            var types = new Mock<IPostTypeRepository>();
            types.Setup(x => x.SelectAll()).Returns<List<PostType>>(null);

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.details = details.Object;
            controller.prov = provinces.Object;
            controller.amp = amphur.Object;
            controller.tmbn = tumbon.Object;
            controller.breeds = breeds.Object;
            controller.coats = coats.Object;
            controller.eyes = eyes.Object;
            controller.lifStages = lifestage.Object;
            controller.patterns = patterns.Object;
            controller.tails = tails.Object;
            controller.types = types.Object;

            var result = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void EditPostTest()
        {
            var user = new User();
            user.Id = 1;

            var post = new PostForm();

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var detail = new PostDetail();
            detail.Post = new Post();
            detail.Cat = new Cat();
            detail.Address = new Address();


            var details = new Mock<IPostDetailRepository>();
            details.Setup(x => x.SelectById(1)).Returns(detail);

            var posts = new Mock<IPostRepository>();
            var cats = new Mock<ICatRepository>();
            var addresses = new Mock<IAddressRepository>();
            var picture = new Mock<IPictureRepository>();

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.posts = posts.Object;
            controller.cats = cats.Object;
            controller.addresses = addresses.Object;
            controller.details = details.Object;
            controller.pictures = picture.Object;

            var result = controller.Edit(post, 1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Forums");

        }
        [TestMethod]
        public void DeleteTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IPostDetailRepository>();
            repo.Setup(x => x.SelectById(1)).Returns(new PostDetail());

            var controller = new ForumController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.details = repo.Object;

            var result = controller.Delete(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Forums");
        }
    }
}
