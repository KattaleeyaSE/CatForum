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
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["Admin"]).Returns(new Admin());
            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IPostDetailRepository>();
            repo.Setup(x => x.SelectAll()).Returns(new List<PostDetail>());

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.detailRepository = repo.Object;

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData["PostRepo"], typeof(IPostDetailRepository));
        }
        [TestMethod()]
        public void LoginTest()
        {
            var controller = new HomeController();
            var result = controller.Login() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod()]
        public void LoginValidTest()
        {
            var sessionValue = new User();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupSet(x => x.Session["User"] = It.IsAny<User>()).Callback((string name, object val) =>
            {
                sessionValue = (User)val;
                context.SetupGet(x => x.Session["User"]).Returns(sessionValue);
            });
            context.SetupGet(x => x.Session["User"]).Returns(null);

            var requestContext = new RequestContext(context.Object, new RouteData());

            User param1 = new User();
            param1.Id = 1;
            param1.Username = "user";
            param1.Password = "1234";

            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Validate(param1)).Returns(param1);

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.repository = repo.Object;

            var result = controller.Login(param1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");
            Assert.AreEqual(sessionValue, param1);
        }
        [TestMethod()]
        public void LoginInvalidTest()
        {
            var sessionValue = new User();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(null);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Validate(null)).Returns<User>(null);

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.repository = repo.Object;

            var result = controller.Login(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod()]
        public void RegisterGetTest()
        {
            var provinces = new Mock<IProvinceRepository>();
            provinces.Setup(x => x.SelectAll()).Returns<User>(null);
            var amphurs = new Mock<IAmphurRepository>();
            amphurs.Setup(x => x.SelectAll()).Returns<List<Amphur>>(null);
            var tumbons = new Mock<ITumbonRepository>();
            amphurs.Setup(x => x.SelectAll()).Returns<List<Tumbon>>(null);

            var controller = new HomeController();
            controller.provinces = provinces.Object;
            controller.amphurs = amphurs.Object;
            controller.tumbons = tumbons.Object;

            var result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod()]
        public void RegisterPostTest()
        {
            var post = new RegisterForm();
            post.Username = "user";
            post.Password = "1234";
            post.RePassword = "1234";
            post.Email = "a@a.a";
            post.ReEmail = "a@a.a";
            post.Province = 1;
            post.Amphur = 1;
            post.Tumbon = 1;

            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Register(It.IsAny<User>())).Returns(1);
            var addresses = new Mock<IAddressRepository>();
            addresses.Setup(x => x.IsAddressExist(1, 1, 1)).Returns<Address>(null);

            var controller = new HomeController();
            controller.repository = repo.Object;
            controller.addressRepository = addresses.Object;

            var result = controller.Register(post) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");
        }
        [TestMethod()]
        public void LogoutTest()
        {
            var sessionValue = new User();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupSet(x => x.Session["User"] = It.IsAny<User>()).Callback((string name, object val) =>
            {
                sessionValue = (User)val;
                context.SetupGet(x => x.Session["User"]).Returns(sessionValue);
            });
            context.SetupGet(x => x.Session["User"]).Returns(new User());
            var requestContext = new RequestContext(context.Object, new RouteData());

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);

            var result = controller.Logout() as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");
            Assert.AreEqual(sessionValue, null);
        }
    }
}
